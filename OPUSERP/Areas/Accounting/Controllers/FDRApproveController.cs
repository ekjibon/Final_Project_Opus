using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class FDRApproveController : Controller
    {
        private readonly IFDRInvestmentService fDRInvestmentService;
        private readonly ICostCentreService costCentreService;

        public FDRApproveController(IFDRInvestmentService fDRInvestmentService, ICostCentreService costCentreService)
        {
            this.fDRInvestmentService = fDRInvestmentService;
            this.costCentreService = costCentreService;
        }

        public async Task<IActionResult> Index()
        {
            FRDApprovalViewModel model = new FRDApprovalViewModel
            {
                fDRInvestmentALLViews = await fDRInvestmentService.GetFDRInvestmentALLView(),
            };
            return View(model);
        }

        public async Task<IActionResult> ApproveFundTransferNew(int fdrMasterId, decimal? amount, string fdrDate, string fdrNo)
        {
            //#region Auto Voucher
            //await costCentreService.FDRCreateAutoVoucher(amount, fdrDate, fdrNo, HttpContext.User.Identity.Name);
            //#endregion

            await fDRInvestmentService.UpdateFDRInvestmentMasterStatusId(fdrMasterId, 1);
            await fDRInvestmentService.UpdateFDRInvestmentDetailsStatusIdByMasterId(fdrMasterId, 1);

            return RedirectToAction("Index", "FDRApprove");
        }

        [HttpPost]
        public async Task<IActionResult> ReturnToFdrFromApprove(int fdrMasterId)
        {
            await fDRInvestmentService.UpdateFDRInvestmentMasterStatusId(fdrMasterId, -1);
            await fDRInvestmentService.UpdateFDRInvestmentDetailsStatusIdByMasterId(fdrMasterId, -1);
            return Json(true);
        }

        public async Task<JsonResult> ApproveFundTransfer(int IDs, string FTInstructionNos, Dictionary<int, string> ISApproveds, Dictionary<int, string> FDRIDs, List<int> TLs, int FTStatus, int AppType)
        {
            try
            {
                var Result = new object();

                string userID = HttpContext.User.Identity.Name;
                await fDRInvestmentService.UpdateFDRInvestmentMasterStatusId(IDs, FTStatus);

                if (TLs != null)
                {
                    var Index = 1;
                    foreach (var key in TLs)
                    {
                        int fdrID = 0;
                        int appStatus = 0;

                        if (FDRIDs.ContainsKey(Index))
                            int.TryParse(FDRIDs[Index], out fdrID);

                        if (ISApproveds.ContainsKey(Index))
                            int.TryParse(ISApproveds[Index], out appStatus);

                        if (AppType != 1 && appStatus == 1)
                        {
                            appStatus = -1;
                        }

                        await fDRInvestmentService.UpdateFDRInvestmentDetailsStatusId(fdrID, appStatus);
                        Index = Index + 1;
                    }
                }
                Result = "Approved Successfully!!";
                return Json(Result.ToString());
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }


        #region API
        [AllowAnonymous]
        [Route("global/api/GetFDRInvestmentDetailByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetFDRInvestmentDetailByMasterId(int id)
        {
            return Json(await fDRInvestmentService.GetFDRInvestmentDetailByMasterId(id));
        }
        #endregion
    }
}