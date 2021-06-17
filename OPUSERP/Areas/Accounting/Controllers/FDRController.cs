using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDRController : Controller
    {
        private readonly IBankBranchService bankBranchService;
        private readonly ILedgerService ledgerService;
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly IBankChargeMasterService bankChargeMasterService;
        private readonly ICostCentreService costCentreService;
        private readonly IVoucherService voucherService;

        public FDRController(ILedgerService ledgerService, IBankBranchService bankBranchService, IBankChargeMasterService bankChargeMasterService, IFDRInvestmentService fDRInvestmentService, ICostCentreService costCentreService, IVoucherService voucherService)
        {
            this.ledgerService = ledgerService;
            this.bankBranchService = bankBranchService;
            this.bankChargeMasterService = bankChargeMasterService;
            this.fDRInvestmentService = fDRInvestmentService;
            this.costCentreService = costCentreService;
            this.voucherService = voucherService;
        }

        public async Task<IActionResult> Index()
        {
            FDRInvestmentViewModel model = new FDRInvestmentViewModel
            {
                //ledgerRelations = await ledgerService.GetLedgerForPayment(),
                bankBranchDetails = await bankBranchService.GetAllBank(),
                //bankChargeMasters = await bankBranchService.GetBankChargeMaster(),
                fDRCalFormulas = await bankBranchService.GetFDRCalFormula(),
                fDRInvestmentALLViews = await fDRInvestmentService.GetFDRInvestmentALLViewForSelf()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] FDRInvestmentViewModel model)
        {

            var fDRMasterId = 0;
            var fDRDetailId = 0;

            FDRInvesmentMaster data = new FDRInvesmentMaster
            {
                Id = Convert.ToInt32(model.fDRInvesmentMasterId),
                bankId = model.bankId,
                bankAccountNo = model.bankAccountNo,
                bankBranchName = model.bankBranchName,
                FTInstructionNo = model.FTInstructionNo,
                FTSendTo = model.FTSendTo,
                FTDate = model.FTDate,
                NOI = model.NOI,
                SOF = model.SOF,
                FDRStatus = 0,
                Status = 0,
                IsJournal = 0
            };

            fDRMasterId = await fDRInvestmentService.SaveFDRInvesmentMaster(data);

            var fdrdetailsByMasterId = await fDRInvestmentService.GetFDRInvestmentDetailByMasterId(fDRMasterId);
            if (fdrdetailsByMasterId.Count() > 0)
            {
                await fDRInvestmentService.DeleteFDRInvestmentDetailByMasterId(fDRMasterId);
            }

            FDRInvestmentDetail details = new FDRInvestmentDetail
            {
                Id = 0,
                fDRInvesmentMasterId = fDRMasterId,
                RefNo = model.RefNo,
                Rate = model.Rate,
                Amount = model.Amount,
                OpenDate = model.OpenDate,
                MaturityDate = model.MaturityDate,
                DestinationType = model.DestinationType,
                bankId = model.detailsbankId,
                DestinationBranch = model.DestinationBranch,
                ChequeNo = model.ChequeNo,
                FormulaType = model.FormulaType,
                MaturityStatus = "New",
                ApprovalStatus = 0,
                TotalInterest = model.TotalInterest,
                Tenure = model.Tenure,
                TenureType = model.TenureType,
                NPBAddress = model.NPBAddress
            };
            fDRDetailId = await fDRInvestmentService.SaveFDRInvestmentDetail(details);

            #region Auto Voucher
            if (model.fDRInvesmentMasterId == 0)
            {
                await costCentreService.FDRCreateAutoVoucher(model.Amount, model.OpenDate.ToString(), model.RefNo, HttpContext.User.Identity.Name);
            }
            #endregion

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFDRByMasterId(int Id, string refNo)
        {
            await fDRInvestmentService.DeleteFDRInvestmentDetailByMasterId(Id);
            await fDRInvestmentService.DeleteFDRInvesmentMasterById(Id);

            var voucherMaster = await voucherService.GetVoucherMasterByRefNo(refNo);
            if (voucherMaster != null)
            {
                int? vmId = voucherMaster.Id;

                var masterId = await voucherService.DeleteVoucher(vmId, "Investment Voucher deleted.", HttpContext.User.Identity.Name);
            }

            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnFdrToApprove(int fdrMasterId)
        {

            await fDRInvestmentService.UpdateFDRInvestmentMasterStatusId(fdrMasterId, 0);
            await fDRInvestmentService.UpdateFDRInvestmentDetailsStatusIdByMasterId(fdrMasterId, 0);
            return Json(true);
            //return RedirectToAction("Index", "FDRApprove");

        }

        [HttpPost]
        public async Task<JsonResult> SaveFDRInvestment(int? IDs, string NOIs, string SOFs, string BankNames, string AccountNos, string FTDates
            , string BranchNames, string FTInstructionNos, string FTSendTos, string FDRStatuss, string IsJournals, List<string> TFTs
            , Dictionary<int, string> Amounts, Dictionary<int, string> Rates, Dictionary<int, string> DTypes
            , Dictionary<int, string> DBanks, Dictionary<int, string> DBranchs, Dictionary<int, string> OpenDates
            , Dictionary<int, string> MaturityDates, Dictionary<int, string> ChequeNos, Dictionary<int, string> FTypes, Dictionary<int, string> TIs
            , Dictionary<int, string> Tenures, Dictionary<int, string> TenureTypes, Dictionary<int, string> NPBAs)
        {
            int Result = 0;
            try
            {
                string userID = HttpContext.User.Identity.Name;
                FDRInvesmentMaster master = new FDRInvesmentMaster();
                master.NOI = NOIs;
                master.SOF = SOFs;
                //master.bankChargeMasterId = Convert.ToInt32(AccountNos);
                master.bankId = Convert.ToInt32(BankNames);
                master.bankAccountNo = AccountNos;
                master.bankBranchName = BranchNames;
                master.FTDate = DateTime.Now;
                master.FTInstructionNo = FTInstructionNos;
                master.FTSendTo = FTSendTos;
                master.FDRStatus = Convert.ToInt32(FDRStatuss);
                master.IsJournal = Convert.ToInt32(IsJournals);
                master.Status = 0;

                if (IDs > 0)
                {
                    master.Id = Convert.ToInt32(IDs);
                    master.updatedBy = userID;
                    master.updatedAt = DateTime.Now;
                    Result = await fDRInvestmentService.SaveFDRInvesmentMaster(master);
                }
                else
                {
                    master.createdBy = userID;
                    master.createdAt = DateTime.Now;
                    Result = await fDRInvestmentService.SaveFDRInvesmentMaster(master);
                }
                List<FDRInvestmentDetail> lstDetails = new List<FDRInvestmentDetail>();
                FDRInvestmentDetail details = new FDRInvestmentDetail();
                if (TFTs != null)
                {
                    if (IDs > 0)
                    {
                        await fDRInvestmentService.DeleteFDRInvestmentDetailByMasterId((int)IDs);
                    }
                    var Index = 1;
                    foreach (var key in TFTs)
                    {
                        decimal AMT = 0;
                        decimal Rate = 0;

                        string dtype = string.Empty;
                        string dbank = string.Empty;
                        string dbranch = string.Empty;
                        string opendate = string.Empty;
                        string maturitydate = string.Empty;
                        string chequeno = string.Empty;
                        string formulaType = string.Empty;
                        decimal totalInterest = 0;
                        string tenure = string.Empty;
                        string tenureType = string.Empty;
                        string npba = string.Empty;

                        if (DTypes.ContainsKey(Index))
                            dtype = DTypes[Index];

                        if (DBanks.ContainsKey(Index))
                            dbank = DBanks[Index];

                        if (DBranchs.ContainsKey(Index))
                            dbranch = DBranchs[Index];

                        if (OpenDates.ContainsKey(Index))
                            opendate = OpenDates[Index];

                        if (MaturityDates.ContainsKey(Index))
                            maturitydate = MaturityDates[Index];

                        if (ChequeNos.ContainsKey(Index))
                            chequeno = ChequeNos[Index];

                        if (Amounts.ContainsKey(Index))
                            decimal.TryParse(Amounts[Index], out AMT);

                        if (Rates.ContainsKey(Index))
                            decimal.TryParse(Rates[Index], out Rate);

                        if (FTypes.ContainsKey(Index))
                            formulaType = FTypes[Index];

                        if (TIs.ContainsKey(Index))
                            decimal.TryParse(TIs[Index], out totalInterest);

                        if (Tenures.ContainsKey(Index))
                            tenure = Tenures[Index];

                        if (TenureTypes.ContainsKey(Index))
                            tenureType = TenureTypes[Index];

                        if (NPBAs.ContainsKey(Index))
                            npba = NPBAs[Index];

                        details.fDRInvesmentMasterId = Result;
                        details.RefNo = NOIs + "/" + Result + "/" + Index;
                        details.DestinationType = dtype;
                        details.ApprovalStatus = 0;
                        details.DestinationBranch = dbranch;
                        if (dtype == "Same Bank")
                        {
                            details.bankId = Convert.ToInt32(BankNames);
                            details.ChequeNo = string.Empty;
                        }
                        else
                        {
                            details.bankId = Convert.ToInt32(dbank);
                            details.ChequeNo = chequeno;
                        }

                        details.OpenDate = Convert.ToDateTime(opendate);
                        details.MaturityDate = Convert.ToDateTime(maturitydate);
                        details.Amount = AMT;
                        details.Rate = Rate;
                        details.FormulaType = formulaType;
                        details.TotalInterest = totalInterest;
                        details.Tenure = tenure;
                        details.TenureType = tenureType;
                        details.NPBAddress = npba;
                        details.ApprovalStatus = 0;
                        details.MaturityStatus = "New";
                        lstDetails.Add(details);
                        details = new FDRInvestmentDetail();
                        Index = Index + 1;
                    }
                    foreach (FDRInvestmentDetail fDRInvestmentDetail in lstDetails)
                    {
                        await fDRInvestmentService.SaveFDRInvestmentDetail(fDRInvestmentDetail);
                    }
                }

                return Json(Result);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        public async Task<IActionResult> GetFTNoByBankName(string bankName, string accNo)
        {
            var Number = "";
            var date = DateTime.Now;
            var fDRInvestment = await fDRInvestmentService.GetFDRInvesmentMaster();
            var bank = bankName.Split(" ");
            var result = accNo.Substring(accNo.Length - 5);
            Number = "FDR/" + bank[0] + "/" + result + "/" + fDRInvestment.Count() + "/" + date.Day + "/" + date.Month + "/" + date.Year;
            return Json(Number);
        }

        [HttpGet]
        public async Task<IActionResult> GetFdrDetails(int id, int appStatus)
        {
            return Json(await fDRInvestmentService.GetFdrDetails(id, appStatus));
        }

        #region
        [AllowAnonymous]
        [Route("global/api/GetBankChargeMasterByBankBranchDetailsId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBankChargeMasterByBankBranchDetailsId(int id)
        {
            return Json(await bankBranchService.GetBankChargeMasterByBankBranchDetailsId(id));
        }
        #endregion
    }
}