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
    public class ReassignToUsersController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IUserInfoes userInfoes;

        public ReassignToUsersController(IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService)
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
                claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1),
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 11, 11),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 8, 8),
                claimAssignedViewModels = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 9, 9),
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
                assignTypeId = 9,
                assignDate = model.AssignDate,
                remarks = "Reassign To Users After Repaire",
                deliveryMode = model.deliveryMode,
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, 9);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = 9,
                description = "Reassign To Users After Repaire"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));

        }
    }
}