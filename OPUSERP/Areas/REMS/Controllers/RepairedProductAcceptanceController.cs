using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class RepairedProductAcceptanceController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfoes;

        public RepairedProductAcceptanceController(IHostingEnvironment hostingEnvironment, IRepairTransactionLogService repairTransactionLogService, IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IClaimAssignService claimAssignService)
        {
            this._hostingEnvironment = hostingEnvironment;
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
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignType(userInfo.UserId, 9, 9),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignType(userInfo.UserId, 13, 13),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int? claimId, int? assignUserId)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1);
            //    model.claimAssigns = await claimAssignService.GetClaimAssign();
            //    return View(model);
            //}
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);

            ClaimAssign data = new ClaimAssign
            {
                claimId = claimId,
                assignUserId = assignUserId,
                assignTypeId = 13,
                assignDate = DateTime.Now,
                remarks = "Product Acceptence"
            };
            var id= await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)claimId, 13);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)claimId,
                statusInfoId = 13,
                description = "Product Acceptence"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return Json(id);
        }
    }
}