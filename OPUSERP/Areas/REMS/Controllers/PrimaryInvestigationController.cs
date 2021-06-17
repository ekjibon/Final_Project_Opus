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
    public class PrimaryInvestigationController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IUserInfoes userInfoes;
        private readonly IRepairTransactionLogService repairTransactionLogService;

        public PrimaryInvestigationController(IClaimRegisterService claimRegisterService, IClaimAssignService claimAssignService, IUserInfoes userInfoes, IRepairTransactionLogService repairTransactionLogService)
        {
            this.claimRegisterService = claimRegisterService;
            this.claimAssignService = claimAssignService;
            this.userInfoes = userInfoes;
            this.repairTransactionLogService = repairTransactionLogService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                AssignToName = User.Identity.Name,
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignType(user.UserId, 2, 2),
                claimAssignViews = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId,3),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 12)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ClaimRegisterViewModel model)
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                model.AssignToName = User.Identity.Name;
                model.claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignType(user.UserId, 2, 2);
                model.claimAssignViews = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 3);
                return View(model);
            }
            
            var assignTypeId = Convert.ToInt32(model.isObsolete);
           
            ClaimAssign data = new ClaimAssign
            {
                claimId = model.claimId,
                problemDescription = model.ProblemDescription,
                organizationId = model.assignToVendorId,
                assignTypeId = assignTypeId,
                assignDate = model.AssignDate,
                assignUserId = model.AssignToId,
                remarks = model.remarks
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, Convert.ToInt32(model.isObsolete));

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = Convert.ToInt32(model.isObsolete),
                description = model.remarks
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));
        }

    }
}