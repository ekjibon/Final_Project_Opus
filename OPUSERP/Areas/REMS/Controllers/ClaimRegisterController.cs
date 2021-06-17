using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class ClaimRegisterController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IUserInfoes userInfoes;
        private readonly IRepairTransactionLogService repairTransactionLogService;

        public ClaimRegisterController(IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IRepairTransactionLogService repairTransactionLogService)
        {
            this.claimRegisterService = claimRegisterService;
            this.userInfoes = userInfoes;
            this.repairTransactionLogService = repairTransactionLogService;
        }

        // GET: SupplierInfo
        public async Task<IActionResult> Index()
        {
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1)
            };
            return View(model);
        }

        // POST: ClaimRegister/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ClaimRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1);
                return View(model);
            }
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);

            var sale = await claimRegisterService.GetClaimRegisterList(1);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.claimDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "Memo" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.SaleNo = issueNo;

            ClaimRegister data = new ClaimRegister
            {
                assetRegistrationId = model.assetRegistrationId,
                claimDescription = model.claimDescription,
                userId = user.UserId,
                statusId = 1,
                claimDate = model.claimDate,
                claimNumber = issueNo,
                isWarrenty = model.warrentyId
            };

            int claimId = await claimRegisterService.SaveClaimRegister(data);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = claimId,
                statusInfoId = 1,
                description = "Claim Register"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));

        }

        // GET: ClaimRegister/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClaimRegister/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClaimRegister/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClaimRegister/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region api
        [Route("global/api/GetClaimRegistration")]
        [HttpGet]
        public async Task<IActionResult> GetClaimRegistration()
        {
            return Json(await claimRegisterService.GetClaimRegisterList(0));
        }

        [Route("global/api/GetAssetRegistration")]
        [HttpGet]
        public async Task<IActionResult> GetAssetRegistration()
        {
            return Json(await claimRegisterService.GetAssetRegistration());
        }

        [Route("global/api/GetAssetWarrenty/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssetWarrenty(int id)
        {
            return Json(await claimRegisterService.GetAssetWarrenty(id));
        }

        [Route("global/api/GetAssetAssign/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAssetAssign(int id)
        {
            return Json(await claimRegisterService.GetAssetAssign(id));
        }
        #endregion
    }
}