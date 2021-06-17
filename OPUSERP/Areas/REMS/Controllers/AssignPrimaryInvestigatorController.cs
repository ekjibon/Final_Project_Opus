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
    public class AssignPrimaryInvestigatorController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IUserInfoes userInfoes;

        public AssignPrimaryInvestigatorController(IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService)
        {
            this.claimRegisterService = claimRegisterService;
            this.userInfoes = userInfoes;
            this.claimAssignService = claimAssignService;
            this.repairTransactionLogService = repairTransactionLogService;
        }

        // GET: RepairHead
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1),
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignUser(userInfo.UserId, 2)
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
                string userName = HttpContext.User.Identity.Name;
                var userInfo = await userInfoes.GetUserInfoByUser(userName);
                model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1);
                model.claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignUser(userInfo.UserId, 2);
                return View(model);
            }
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);

            ClaimAssign data = new ClaimAssign
            {
                claimId = model.claimId,
                problemDescription = model.ProblemDescription,
                assignUserId = model.AssignToId,
                assignTypeId = 2,
                assignDate = model.AssignDate,
                remarks = "Assign To Employee for Primary Investigation"
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, 2);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = 2,
                description = "Assign To Employee for Primary Investigation"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));

        }
    }
}