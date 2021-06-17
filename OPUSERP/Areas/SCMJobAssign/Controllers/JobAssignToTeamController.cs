using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMJobAssign.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail.interfaces;

namespace OPUSERP.Areas.SCMJobAssign.Controllers
{
    [Area("SCMJobAssign")]
    public class JobAssignToTeamController : Controller
    {
        private readonly IRequisitionService requisitionService;
        private readonly IUserInfoes userInfoes;
        private readonly RequisitionStatusHistory requisitionStatusHistory;
        private readonly ISCMTeamService teamService;
        private readonly ISCMMailService sCMMailService;

        public JobAssignToTeamController(IRequisitionService requisitionService, RequisitionStatusHistory requisitionStatusHistory, IUserInfoes userInfoes, ISCMTeamService teamService, ISCMMailService sCMMailService)
        {
            this.requisitionService = requisitionService;
            this.userInfoes = userInfoes;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.teamService = teamService;
            this.sCMMailService = sCMMailService;
        }

        // GET: JobAssignToTeam
        public async Task<ActionResult> Index()
        {
            JobAssignViewModel model = new JobAssignViewModel
            {
                requisitionMasters = await requisitionService.GetRequisitionMasterListForAssign(),
                assignRequisitionMasters = await requisitionService.GetRequisitionMasterListByStatus(6),
                teamMasters = await teamService.GetAllTeamMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] JobAssignViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var currUserInfo = await userInfoes.GetUserInfoByUser(userName);

            if (!ModelState.IsValid || model.masterIds==null)
            {
                model.requisitionMasters = await requisitionService.GetRequisitionMasterListForAssign();
                model.assignRequisitionMasters = await requisitionService.GetRequisitionMasterListByStatus(6);
                model.teamMasters = await teamService.GetAllTeamMaster();
                if (model.masterIds == null)
                {
                    ModelState.AddModelError(string.Empty, "You have to assign minimum 1 team");
                }

                return View(model);
            }
            //return Json(model);
            List<int?> lstLeader = new List<int?>(); 
            for(int i = 0; i < model.masterIds.Length; i++)
            {
                requisitionService.AssignTeamInRequisitionMasterById((int)model.masterIds[i],6,(int)model.teamIds[i]);

                var requisitionMasters = await requisitionService.GetRequisitionMasterById((int)model.masterIds[i]);

                var teamMasters = await teamService.GetTeamMasterById((int)model.teamIds[i]);
                var nextUserInfo = await userInfoes.GetUserInfoByUserId(teamMasters.leaderId);
                
                string empNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;                
                await requisitionStatusHistory.SaveRequisitionStatusLog((int)model.masterIds[i], 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, "", "", 6, "PR", (int)model.masterIds[i], requisitionMasters.reqNo);
                lstLeader.Add(teamMasters.leaderId);
                if (!lstLeader.Contains(teamMasters.leaderId))
                {
                    string host = HttpContext.Request.Host.ToString();
                    string scheme = Request.Scheme;
                    string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                    await sCMMailService.MailMessage(nextUserInfo.Email, requisitionMasters.reqNo, 6, empNameCode, baseUrl);
                }
                
            }
            TempData["Success"] = "Assigned Successfully!";
            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> JobAssignToBuyer()
        {
            JobAssignViewModel model = new JobAssignViewModel
            {
                assignRequisitionMasters = await requisitionService.GetRequisitionMasterListByStatus(6),
                requisitionMasters = await requisitionService.GetRequisitionMasterListByStatus(7)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobAssignToBuyer([FromForm] JobAssignViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var currUserInfo = await userInfoes.GetUserInfoByUser(userName);

            //return Json(model);
            if (!ModelState.IsValid )
            {
                model.assignRequisitionMasters = await requisitionService.GetRequisitionMasterListByStatus(6);
                return View(model);
            }
            //return Json(model);
            if (model.rBuyer == 1)
            {
                for (int i = 0; i < model.reqDetailIds.Length; i++)
                {
                    requisitionService.AssignTeamInRequisitionDetailsById((int)model.reqDetailIds[i], 7, (int)model.singleMemberIds);

                   
                }
                var requisitionDetail = await requisitionService.GetRequisitionDetailById((int)model.reqDetailIds[0]);

                var memberUsers = await teamService.GetTeamMemberById((int)model.singleMemberIds);
                var nextUserInfo = await userInfoes.GetUserInfoByUserId(memberUsers.memberId);
                string empNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;

                await requisitionStatusHistory.SaveRequisitionStatusLog(requisitionDetail.requisitionMasterId, 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, "", "", 7, "PR", requisitionDetail.requisitionMasterId, requisitionDetail.requisitionMaster.reqNo);

                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                await sCMMailService.MailMessage(nextUserInfo.Email, requisitionDetail.requisitionMaster.reqNo.ToString(), 7, empNameCode, baseUrl);
            }
            else
            {
                for (int i = 0; i < model.reqDetailIds.Length; i++)
                {
                    requisitionService.AssignTeamInRequisitionDetailsById((int)model.reqDetailIds[i], 7, (int)model.MemberIds[i]);

                  
                }
                var requisitionDetail = await requisitionService.GetRequisitionDetailById((int)model.reqDetailIds[0]);

                var memberUsers = await teamService.GetTeamMemberById((int)model.MemberIds[0]);
                var nextUserInfo = await userInfoes.GetUserInfoByUserId(memberUsers.memberId);
                string empNameCode = nextUserInfo.EmpCode + "-" + nextUserInfo.EmpName;

                await requisitionStatusHistory.SaveRequisitionStatusLog(requisitionDetail.requisitionMasterId, 1, Convert.ToInt32(currUserInfo.UserTypeId), currUserInfo.UserId, empNameCode, "", "", 7, "PR", requisitionDetail.requisitionMasterId, requisitionDetail.requisitionMaster.reqNo);

                string host = HttpContext.Request.Host.ToString();
                string scheme = Request.Scheme;
                string baseUrl = $"" + scheme + "://" + host + "/Auth/Account/Login";
                await sCMMailService.MailMessage(nextUserInfo.Email, requisitionDetail.requisitionMaster.reqNo.ToString(), 7, empNameCode, baseUrl);
            }

            return RedirectToAction(nameof(JobAssignToBuyer));
        }

        public ActionResult JobReturnToTeam(int id)
        {
            requisitionService.ReturnTeamInRequisitionMasterById(id);
            return RedirectToAction(nameof(JobAssignToBuyer));
        }


        #region API

        [HttpGet]
        public async Task<IActionResult> GetMasterWiseRequisitionDetails(string MasterId)
        {
            var Re_DetailList = await requisitionService.GetAllItemListByRequisitionId(Convert.ToInt32(MasterId));
            return Json(Re_DetailList);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamMemberForJobAssignByUser(string teamId)
        {
            var Re_DetailList = await teamService.GetAllTeamMemberByMasterId(Convert.ToInt32(teamId));
            return Json(Re_DetailList);
        }

        [HttpGet]
        public async Task<IActionResult> GetMasterWiseRequisitionAttachment(string MasterId)
        {
            var Re_DetailList = await requisitionService.GetDocumentAttachmentList(Convert.ToInt32(MasterId));
            return Json(Re_DetailList);
        }

        [Route("global/api/GetRequisitorInfoByRequisitionId/{reqId}")]
        [HttpGet]
        public async Task<IActionResult> GetRequisitorInfoByRequisitionId(int reqId)
        {
            return Json(await requisitionService.GetRequisitorInfoByRequisitionId(reqId));
        }

        #endregion

    }
}