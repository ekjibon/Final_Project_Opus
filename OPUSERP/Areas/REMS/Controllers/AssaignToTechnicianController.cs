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
    public class AssaignToTechnicianController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IUserInfoes userInfoes;

        public AssaignToTechnicianController(IClaimRegisterService claimRegisterService, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService, IUserInfoes userInfoes)
        {
            this.claimRegisterService = claimRegisterService;
            this.claimAssignService = claimAssignService;
            this.repairTransactionLogService = repairTransactionLogService;
            this.userInfoes = userInfoes;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                AssignToName = User.Identity.Name,
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignType(user.UserId, 4, 4),
                claimAssignViews = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 6),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 11),
                claimAssignWarrentyViewModels = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 10)
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
                model.claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignType(user.UserId, 1, 2);
                model.claimAssignViews = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 6);
                model.claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 12);
                model.claimAssignWarrentyViewModels = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 10);
                return View(model);
            }

            //return Json(model);

            //var assignTypeId = 2;
            //var status = 4;
            //if (model.isObsolete == 1) {
            //    assignTypeId = 3;
            //    status = 3;
            //}

            ClaimAssign data = new ClaimAssign
            {
                claimId = model.claimId,
                problemDescription = model.ProblemDescription,
                organizationId = model.assignToVendorId,
                assignTypeId = model.isObsolete,
                assignDate = model.AssignDate,
                assignUserId = model.AssignToId,
                remarks = model.remarks
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, (int)model.isObsolete);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = model.isObsolete,
                description = model.remarks
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));
        }


    }
}