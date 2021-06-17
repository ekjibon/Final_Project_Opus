using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.SCMRequisition.Models.Lang;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;
//using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Supplier.Interface;

namespace OPUSERP.Areas.Accounting.Controllers
{

    [Area("Accounting")]
    public class NonPOTransactionController : Controller
    {
        private readonly IItemsService itemsService;
        private readonly IAccountingReportService reportingService;
        private readonly IUserInfoes userInfoes;
        private readonly IRateTypeService rateTypeService;
        private readonly ISalaryService salaryService;
        private readonly IVatTaxRateService vatTaxRateService;
        private readonly IUserInfoes userInfo;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IOrganizationService organizationService;
        private readonly IDailyBillReceiveService dailyBillReceiveService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ILedgerService ledgerService;
        private readonly ICostCentreService costCentreService;
        private readonly IVoucherService voucherService;
      


        private readonly HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;
        public NonPOTransactionController(IDailyBillReceiveService dailyBillReceiveService, IAccountingReportService reportingService, IUserInfoes userInfo, IVoucherService voucherService, ICostCentreService costCentreService, ILedgerService ledgerService, IPersonalInfoService personalInfoService, HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService, IItemsService itemsService, IUserInfoes userInfoes, IOrganizationService organizationService, IHostingEnvironment hostingEnvironment, IRateTypeService rateTypeService, IVatTaxRateService vatTaxRateService, ISalaryService salaryService,  IERPCompanyService eRPCompanyService, IConverter converter)

        {
            this.itemsService = itemsService;
            this.userInfoes = userInfoes;
            this.organizationService = organizationService;
            this.dailyBillReceiveService = dailyBillReceiveService;
            this.designationDepartmentService = designationDepartmentService;
            this.personalInfoService = personalInfoService;
            this.rateTypeService = rateTypeService;
            this.costCentreService = costCentreService;
            this.voucherService = voucherService;
            this.salaryService = salaryService;
            this.eRPCompanyService = eRPCompanyService;
            this.vatTaxRateService = vatTaxRateService;
            this.ledgerService = ledgerService;
            this.reportingService = reportingService;
            this.userInfo = userInfo;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Rate Type
        public async Task<IActionResult> RateType()
        {
            RateTypeViewModel model = new RateTypeViewModel
            {
                rateTypes = await rateTypeService.GetrateType()
            };
            return View(model);
        }

        // POST: RateType/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateType([FromForm] RateTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.rateTypes = await rateTypeService.GetrateType();
                return View(model);
            }

            RateType data = new RateType
            {
                Id = model.rateTypeId,
                RateTypeName = model.rateTypeName
            };

            await rateTypeService.SaveRateType(data);

            return RedirectToAction(nameof(RateType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteRateTypeById(int Id)
        {
            await rateTypeService.DeleteRateTypeById(Id);
            return Json(true);
        }

        #endregion

        #region Vat Tax Rate

        public async Task<IActionResult> VatTaxRate()
        {
            VatTaxRateViewModel model = new VatTaxRateViewModel
            {
                vatTaxRates = await vatTaxRateService.GetvatTaxRate(),
                rateTypes = await rateTypeService.GetrateType(),
                taxYears = await salaryService.GetAllTaxYear()
            };
            return View(model);
        }

        // POST: VatTaxRate/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VatTaxRate([FromForm] VatTaxRateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.vatTaxRates = await vatTaxRateService.GetvatTaxRate();
                model.rateTypes = await rateTypeService.GetrateType();
                model.taxYears = await salaryService.GetAllTaxYear();
                return View(model);
            }

            VatTaxRate data = new VatTaxRate
            {
                Id = model.vatTaxRateId,
                rateTypeId = model.rateTypeId,
                taxYearId = model.taxYearId,
                rate = model.rate
            };

            await vatTaxRateService.SavevatTaxRate(data);

            return RedirectToAction(nameof(VatTaxRate));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteVatTaxRateById(int Id)
        {
            await vatTaxRateService.DeleteVatTaxRateById(Id);
            return Json(true);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Index(int? Id)
        {
            if (Id == null)
            {
                Id = 0;
            }
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var dailybill = await dailyBillReceiveService.GetdailyBillReceivebyId((int)Id);
            string Slno = "";
            string processNo = "";
            var sldata = await dailyBillReceiveService.GetSLNumber();
            var processdata = await dailyBillReceiveService.GetProcessNumber();
            if (dailybill != null)
            {
                Slno = dailybill.SLNo;
                processNo = dailybill.ProcessNo;
            }
            else
            {
                Slno = sldata.SlNo;
                processNo = processdata.ProcessNo;
                dailybill = new DailyBillReceive();
            }
            var model = new DailyBillReceiveViewModel
            {
                items = await itemsService.GetAllItem(),
                organizations = await organizationService.GetAllOrganization(),
                SLNo = Slno,
                ProcessNo = processNo,
                dailyBillReceive = dailybill

            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index([FromForm] DailyBillReceiveViewModel model)
        {

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            if (model.supplierId > 0)
            {
                model.vendorstatus = "Vendor";
            }
            else
            {
                model.vendorstatus = "Non Vendor";
            }
            try
            {
                DailyBillReceive data = new DailyBillReceive
                {
                    Id = (int)model.billReceiveId,
                    SLNo = model.SLNo,

                    ProcessNo = model.ProcessNo,
                    BillReceiveDate = model.BillReceiveDate,
                    supplierId = model.supplierId,
                    vendorstatus = model.vendorstatus,
                    IsPo = model.IsPo,
                    IsClaimed = model.IsClaimed,
                    isClaimable = model.isClaimable,
                    ischallan = model.ischallan,
                    GBamount = model.GBamount,
                    ChalanDate = model.ChalanDate,
                    vatNumber = model.vatNumber,
                    Commission = model.Commission,
                    BillStatusId = 0,
                    Vat = model.Vat,
                    Other = model.Other,
                    Discount = model.Discount,
                    Total = model.Total,
                    InvoiceNo = model.InvoiceNo,
                    PoNo = model.PoNo,
                    SentToDepDate = model.SentToDepDate,
                    ResposneemployeeInfoId = model.ResposneemployeeInfoId,
                    employeeInfoId = model.employeeInfoId,
                    ReceiveDepDate = model.ReceiveDepDate,
                    VendorPayReceiveDate = model.VendorPayReceiveDate,
                    CheckReceiveDate = model.CheckReceiveDate,
                    BillReturnDate = model.BillReturnDate,
                    ChequeNo = model.ChequeNo,
                    ReturnComments = model.ReturnComments,
                    PaymentDate = model.PaymentDate,
                    NoReturn = model.NoReturn,
                    NoVat = model.NoVat,
                    NonDepartmentId = model.NonDepartmentId,
                    ExpectedFindate = model.ExpectedFindate,
                    UserDeptCode = model.UserDeptCode,
                    ItemId = model.itemId,
                    ReturnProcessNo = model.ReturnProcessNo

                };
                int MID = await dailyBillReceiveService.SavedailyBillReceive(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(0);
        }


        public async Task<IActionResult> DailyBillReceiveList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var model = new DailyBillReceiveViewModel
            {
                dailyBillReceives = await dailyBillReceiveService.GetdailyBillReceive(),

            };
            return View(model);
        }

        public async Task<IActionResult> BillReceiveReport(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            //var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var model = new BillReceiveViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                dailyBillReceive = await dailyBillReceiveService.GetdailyBillReceivebyId(id)
            };
            return View(model);
        }

        //public async Task<IActionResult> DailyBillReceiveReport(int supplierId,  DateTime fromDate, DateTime toDate)
        //{
           
        //    //ViewBag.InWord = AmountInWord.ConvertToWords(Val);
        //    var model = new DailyBillReceiveReportViewModel
        //    {
        //        //VoucherMaster = await voucherService.GetGetvoucherMasterById(MasterId),
        //        //GetVoucherDetailsByMasterId = await voucherService.GetvoucherDetailByMasterId(MasterId),
        //        companies = await eRPCompanyService.GetAllCompany(),

        //    };
        //    return View();
        //}

        [AllowAnonymous]
        public async Task<ActionResult> DailyBillReceiveReport(int supplierId, DateTime fromDate, DateTime toDate)
        {
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            //companies = await eRPCompanyService.GetAllCompany();
            var data= await reportingService.GetdailyBillReceivePdf(supplierId, fromDate, toDate);
            if (data == null)
            {
                data= new List<DailyBillReceiveReportViewModel>();
            }
            //var model = await reportingService.GetdailyBillReceivePdf(supplierId, fromDate, toDate);
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> InvoicePayment([FromForm] DailyBillReceiveViewModel model)
        {
            string userName = User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var salaryYear = await salaryService.GetAllSalaryYear();
            var taxYear = await salaryService.GetAllTaxYear();
            var company = await eRPCompanyService.GetAllCompany();
            decimal invoiceamount = (decimal)model.Total + (decimal)model.vatCurrentAmount;
            var voucherNo = await ledgerService.GetAutoNumber((int)model.hdnPaymentAccId, 6, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
            var organization = await organizationService.GetOrganizationById((int)model.supplierId);
            VoucherMaster voucherMaster = new VoucherMaster
            {
                Id = 0,
                voucherNo = voucherNo.autoNumber,
                refNo = "NonPoRef - Payment -" + model.billReceiveId,
                voucherDate = model.BillReceiveDate,
                voucherTypeId = 6,
                remarks = model.narration,
                voucherAmount = invoiceamount,
                isPosted = 1,
                companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                fiscalYearId = salaryYear.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                taxYearId = taxYear.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                fundSourceId = 1,
                //specialBranchUnitId= userInfos.specialBranchUnitId
            };
            int VMID = await voucherService.SavevoucherMaster(voucherMaster);


            //1016
            //1012
            if (model.rebatable == 0 && model.deductable == 1 && model.taxExempted == 0 && model.taxInclusive == 1)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);
                if (model.vatCurrentAmount != 0)
                {
                    VoucherDetail voucherDetail = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = 1016, //Convert.ToInt32(model.hdnPaymentAccId),
                        transectionModeId = 1,
                        amount = (decimal)model.vatCurrentAmount,
                        accountName = ledgerrel.ledger.accountName + '(' + ledgerrel.ledger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid = await voucherService.SavevoucherDetail(voucherDetail);
                }
                if (model.vatAmount != 0)
                {
                    VoucherDetail voucherDetail1 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerrelvat.Id,
                        transectionModeId = 2,
                        amount = model.vatAmount,
                        accountName = ledgerrelvat.ledger.accountName + '(' + ledgerrelvat.ledger.accountCode + ')' + '-' + ledgerrelvat.subLedger.accountName + '(' + ledgerrelvat.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid1 = await voucherService.SavevoucherDetail(voucherDetail1);
                }
                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total + (decimal)model.vatCurrentAmount - (decimal)model.vatAmount - (decimal)model.taxAmount,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
                if (model.taxAmount != 0)
                {
                    VoucherDetail voucherDetail4 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerreltax.Id,
                        transectionModeId = 2,
                        amount = model.taxAmount,
                        accountName = ledgerreltax.ledger.accountName + '(' + ledgerreltax.ledger.accountCode + ')' + '-' + ledgerreltax.subLedger.accountName + '(' + ledgerreltax.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid4 = await voucherService.SavevoucherDetail(voucherDetail4);
                }
            }

            if (model.rebatable == 0 && model.deductable == 0 && model.taxExempted == 0 && model.taxInclusive == 1)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);
                if (model.vatCurrentAmount != 0)
                {
                    VoucherDetail voucherDetail = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = 1016, //Convert.ToInt32(model.hdnPaymentAccId),
                        transectionModeId = 1,
                        amount = (decimal)model.vatCurrentAmount,
                        accountName = ledgerrel.ledger.accountName + '(' + ledgerrel.ledger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid = await voucherService.SavevoucherDetail(voucherDetail);
                }
               
                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total + (decimal)model.vatCurrentAmount  - (decimal)model.taxAmount,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
                if (model.taxAmount != 0)
                {
                    VoucherDetail voucherDetail4 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerreltax.Id,
                        transectionModeId = 2,
                        amount = model.taxAmount,
                        accountName = ledgerreltax.ledger.accountName + '(' + ledgerreltax.ledger.accountCode + ')' + '-' + ledgerreltax.subLedger.accountName + '(' + ledgerreltax.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid4 = await voucherService.SavevoucherDetail(voucherDetail4);
                }
            }
            if (model.rebatable == 1 && model.deductable == 0 && model.taxExempted == 0 && model.taxInclusive == 1)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);
               

                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total  - (decimal)model.taxAmount,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
                if (model.taxAmount != 0)
                {
                    VoucherDetail voucherDetail4 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerreltax.Id,
                        transectionModeId = 2,
                        amount = model.taxAmount,
                        accountName = ledgerreltax.ledger.accountName + '(' + ledgerreltax.ledger.accountCode + ')' + '-' + ledgerreltax.subLedger.accountName + '(' + ledgerreltax.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid4 = await voucherService.SavevoucherDetail(voucherDetail4);
                }
            }
            if (model.rebatable == 1 && model.deductable == 0 && model.taxExempted == 1 && model.taxInclusive == 1)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);


                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
              
            }

            if (model.rebatable == 1 && model.deductable == 1 && model.taxExempted == 0 && model.taxInclusive == 1)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);
               
                if (model.vatAmount != 0)
                {
                    VoucherDetail voucherDetail1 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerrelvat.Id,
                        transectionModeId = 2,
                        amount = model.vatAmount,
                        accountName = ledgerrelvat.ledger.accountName + '(' + ledgerrelvat.ledger.accountCode + ')' + '-' + ledgerrelvat.subLedger.accountName + '(' + ledgerrelvat.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid1 = await voucherService.SavevoucherDetail(voucherDetail1);
                }
                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total - (decimal)model.vatAmount - (decimal)model.taxAmount,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
                if (model.taxAmount != 0)
                {
                    VoucherDetail voucherDetail4 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerreltax.Id,
                        transectionModeId = 2,
                        amount = model.taxAmount,
                        accountName = ledgerreltax.ledger.accountName + '(' + ledgerreltax.ledger.accountCode + ')' + '-' + ledgerreltax.subLedger.accountName + '(' + ledgerreltax.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid4 = await voucherService.SavevoucherDetail(voucherDetail4);
                }
            }


            if (model.rebatable == 1 && model.deductable == 1 && model.taxExempted == 0 && model.taxInclusive == 0)
            {
                var ledgerrelM = await ledgerService.GetledgerRelationById((int)model.hdnPaymentAccId);
                var ledgerrel = await ledgerService.GetledgerRelationById(1016);
                var ledgerrelvat = await ledgerService.GetledgerRelationByLedgerSubledgerId(1005, (int)model.subLedgerVatId);
                var ledgerrelvatpro = await ledgerService.GetledgerRelationByLedgerSubledgerId(1012, (int)organization.ledgerId);
                var ledgerreltax = await ledgerService.GetledgerRelationByLedgerSubledgerId(1006, (int)model.subLedgerTaxId);

                if (model.vatAmount != 0)
                {
                    VoucherDetail voucherDetail1 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerrelvat.Id,
                        transectionModeId = 2,
                        amount = model.vatAmount,
                        accountName = ledgerrelvat.ledger.accountName + '(' + ledgerrelvat.ledger.accountCode + ')' + '-' + ledgerrelvat.subLedger.accountName + '(' + ledgerrelvat.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid1 = await voucherService.SavevoucherDetail(voucherDetail1);
                }
                VoucherDetail voucherDetail2 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = ledgerrelvat.Id,
                    transectionModeId = 1,
                    amount = (decimal)model.Total,
                    accountName = ledgerrelvatpro.ledger.accountName + '(' + ledgerrelvatpro.ledger.accountCode + ')' + '-' + ledgerrelvatpro.subLedger.accountName + '(' + ledgerrelvatpro.subLedger.accountCode + ')',
                    isPrincAcc = 0

                };
                int vid2 = await voucherService.SavevoucherDetail(voucherDetail2);

                VoucherDetail voucherDetail3 = new VoucherDetail
                {
                    Id = 0,
                    voucherId = VMID,
                    ledgerRelationId = (int)model.hdnPaymentAccId,
                    transectionModeId = 2,
                    amount = (decimal)model.Total - (decimal)model.vatAmount - (decimal)model.taxAmount,
                    accountName = ledgerrelM.ledger.accountName + '(' + ledgerrelM.ledger.accountCode + ')',
                    isPrincAcc = 1

                };
                int vid3 = await voucherService.SavevoucherDetail(voucherDetail3);
                if (model.taxAmount != 0)
                {
                    VoucherDetail voucherDetail4 = new VoucherDetail
                    {
                        Id = 0,
                        voucherId = VMID,
                        ledgerRelationId = ledgerreltax.Id,
                        transectionModeId = 2,
                        amount = model.taxAmount,
                        accountName = ledgerreltax.ledger.accountName + '(' + ledgerreltax.ledger.accountCode + ')' + '-' + ledgerreltax.subLedger.accountName + '(' + ledgerreltax.subLedger.accountCode + ')',
                        isPrincAcc = 0

                    };
                    int vid4 = await voucherService.SavevoucherDetail(voucherDetail4);
                }
            }

            await dailyBillReceiveService.UpdatedailyBillReceive(model.billReceiveId, VMID);
            return Json(0);
        }
        public async Task<IActionResult> InvoicePayment()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            var model = new DailyBillReceiveViewModel
            {
                dailyBillReceives = await dailyBillReceiveService.GetdailyBillReceive(),
                VatTaxRates = await vatTaxRateService.GetvatTaxRate(),
                taxYears = await salaryService.GetAllTaxYear(),
                costCentres = await costCentreService.GetCostCentres(),
                GetVoucherMasters=await voucherService.GetvoucherMaster() 


            };
            return View(model);
        }

        public IActionResult BillReceivePdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Accounting/NonPOTransaction/BillReceiveReport?id=" + id;
            //string url = scheme + "://" + host + "/HRPMSLeave/LeaveRegister/BillReceiveReport?year=" + year + "&&empId=" + empId;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public IActionResult DailyBillReceivePdf(int supplierId, DateTime fromDate, DateTime toDate)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/Accounting/NonPOTransaction/DailyBillReceiveReport?supplierId=" + supplierId + "&fromDate=" + fromDate + "&toDate=" + toDate;
            //string url = scheme + "://" + host + "/HRPMSLeave/LeaveRegister/BillReceiveReport?year=" + year + "&&empId=" + empId;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        #region API Section

        [Route("global/api/getItem")]
        [HttpGet]
        public async Task<IActionResult> getItem()
        {

            var result = await itemsService.GetAllItem();
            return Json(result);
        }

        [Route("global/api/GetSupplier")]
        [HttpGet]
        public async Task<IActionResult> GetSupplier()
        {

            var result = await organizationService.GetAllOrganization();
            return Json(result);
        }
        [Route("global/api/GetEmployeer")]
        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {

            var result = await personalInfoService.GetEmployeeInfo();
            return Json(result);
        }
        [Route("global/api/GetDepartment")]
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {

            var result = await designationDepartmentService.GetDepartment();
            return Json(result);
        }
        [Route("global/api/GetBill/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetBill(int Id)
        {

            var result = await dailyBillReceiveService.GetdailyBillReceivebyId(Id);
            return Json(result);
        }

        [Route("global/api/VATLedger")]
        [HttpGet]
        public async Task<IActionResult> VATLedger()
        {

            var result = await ledgerService.GetSubledgerR();
            return Json(result.Where(x => x.subLedger?.haveSubLedger == 2 && x.ledger?.accountName == "VAT Payable"));
        }
        [Route("global/api/TAXLedger")]
        [HttpGet]
        public async Task<IActionResult> TAXLedger()
        {

            var result = await ledgerService.GetSubledgerR();
            return Json(result.Where(x => x.subLedger?.haveSubLedger == 2 && x.ledger?.accountName == "Tax Payable"));
        }
        [Route("global/api/VATCal/{BasePrice}/{VATRate}/{VATExempted}/{VATInclusive}/{MushokProvided}/{Rebatable}/{Deductable}/{VATPayBy}/{Rebate}")]
        [HttpGet]
        public async Task<IActionResult> VATCal(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal MushokProvided, decimal Rebatable, decimal Deductable, decimal VATPayBy, decimal Rebate)
        {

            var result = await dailyBillReceiveService.GetDailyBillPaymentViewModel(BasePrice, VATRate, VATExempted, VATInclusive, MushokProvided, Rebatable, Deductable, VATPayBy, Rebate);
            return Json(result);
        }
        [Route("global/api/TAXCal/{BasePrice}/{VATRate}/{VATExempted}/{VATInclusive}/{VATPayBy}/{vendorId}/{taxyearId}/{calId}/{BaseCalculatePrice}")]
        [HttpGet]
        public async Task<IActionResult> TAXCal(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal VATPayBy, int vendorId, int taxyearId, int calId, decimal BaseCalculatePrice)
        {

            var result = await dailyBillReceiveService.GetDailyBillPaymentViewModelTax(BasePrice, VATRate, VATExempted, VATInclusive, VATPayBy, vendorId, taxyearId, calId, BaseCalculatePrice);
            return Json(result);
        }

        #endregion
    }
}