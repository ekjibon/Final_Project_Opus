using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class AssignNonWarrentyVendorController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IUserInfoes userInfoes;

        public AssignNonWarrentyVendorController(IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService)
        {
            this.claimRegisterService = claimRegisterService;
            this.userInfoes = userInfoes;
            this.claimAssignService = claimAssignService;
            this.repairTransactionLogService = repairTransactionLogService;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 6,6),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(userInfo.UserId, 7)
            };
            return View(model);
        }

        // POST: RepairHead/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ClaimRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1);
                model.claimAssigns = await claimAssignService.GetClaimAssign();
                return View(model);
            }
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);

            ClaimAssign data = new ClaimAssign
            {
                claimId = model.claimId,
                problemDescription = model.ProblemDescription,
                assignUserId = model.AssignToId,
                organizationId = model.assignToVendorId,
                assignTypeId = 7,
                assignDate = model.AssignDate,
                remarks = "Assign Non Warrenty Vendor for Repaire",
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, 7);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = 7,
                description = "Assign Non Warrenty Vendor for Repaire"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));

        }
    }
}