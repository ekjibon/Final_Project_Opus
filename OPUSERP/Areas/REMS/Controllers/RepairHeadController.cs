using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class RepairHeadController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IUserInfoes userInfoes;

        public RepairHeadController(IClaimRegisterService claimRegisterService, IUserInfoes userInfoes, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService)
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
                claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(3),
                claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignUser(userInfo.UserId,4),
                claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(userInfo.UserId,5)
            };
            return View(model);
        }

        // POST: RepairHead/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ClaimRegisterViewModel model)
        {
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(3);
                model.claimAssignViewModels = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 4);
                model.claimAssignViewModelsNew = await claimAssignService.GetClaimAssignListByAssignUser(user.UserId, 5);
                return View(model);
            }
            
           

            int assignType = 4;
            string remark = "Assign To Employee for Repair Head";
            if (model.isWarrenty == 1)
            {
                assignType = 5;
                remark = "Assign To Vendor for Repair Head";
            }
            else
            {
                assignType = 4;
                remark = "Assign To Employee for Repair Head";
            }
            //if ((int)model.claimAssaigneId > 0)
            //{
            //   await claimAssignService.DeleteClaimAssignById((int)model.claimAssaigneId);
            //}

            ClaimAssign data = new ClaimAssign();
            //data.Id = (int)model.claimAssaigneId;
            data.claimId = model.claimId;
            data.problemDescription = model.ProblemDescription;
            if(model.isWarrenty==1)
            {
                data.organizationId = model.assignToVendorId;
                data.assignUserId = user.UserId;
            }
            else
            {
                data.assignUserId = model.AssignToId;
            }
            data.assignTypeId = assignType;
            data.assignDate = model.AssignDate;
            data.remarks = remark;
            
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, assignType);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = assignType,
                description = remark
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            return RedirectToAction(nameof(Index));

        }

        #region api
        [Route("global/api/GetUserInfo")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            return Json(await userInfoes.GetUsersByEmployeeInfo());
        }

        [Route("global/api/GetTransactionLog/{claimId}")]
        [HttpGet]
        public async Task<IActionResult> GetTransactionLog(int claimId)
        {
            return Json(await repairTransactionLogService.GetRepairTransactionLogByClaim(claimId));
        }

        #endregion
    }
}