using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.ERPServices.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class CRMTeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IUserInfoes userInfoes;
        private readonly ICommunicationService communicationService;
        private readonly IModuleAssignService moduleAssignService;

        public CRMTeamController(ITeamService teamService, IUserInfoes userInfoes, IModuleAssignService moduleAssignService, ICommunicationService communicationService)
        {
            this.teamService = teamService;
            this.userInfoes = userInfoes;
            this.communicationService = communicationService;
            this.moduleAssignService = moduleAssignService;
        }

        public async Task<IActionResult> CreateTeam()
        {
            var team = await teamService.GetTeamInfoByTeamId(null);
            int Cteam = 0;
            Cteam = team.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy");
            string autoTeamCode =idate + '-' + (Cteam + 1);

            CRMTeamViewModel model = new CRMTeamViewModel()
            {
                teams = await teamService.GetTeamInfoByTeamId(null),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                areas = await communicationService.GetAllArea(),
                eRPModules=await moduleAssignService.GetERPModules(),
                GetTeams=await teamService.GetAllTeam()
            };
            ViewBag.autoTeamCode = autoTeamCode;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeam([FromForm] CRMTeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.teams = await teamService.GetTeamInfoByTeamId(null);
                model.aspNetUsers = await userInfoes.GetUserInfoByModule(2);
                return View(model);
            }

            Team data = new Team
            {
                Id = Convert.ToInt32(model.tId),
                aspnetuserId = model.aspnetuserId,
                areaId = model.areaId,
                memberName = model.memberName,
                teamCode = model.teamCode,
                moduleId=model.moduleId,
                teamId=model.teamId,
                isActive = 1

            };

            await teamService.SaveTeam(data);
            return RedirectToAction(nameof(CreateTeam));
        }


        #region Assign Team

        public async Task<IActionResult> AssignTeam()
        {
            CRMTeamViewModel model = new CRMTeamViewModel()
            {
                teams = await teamService.GetTeamInfoByTeamId(null),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2)                
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> SaveAssignTeam([FromForm] CRMTeamViewModel model)
        {
            try
            {
                int teamId = 0;

                var teamCheck = await teamService.GetTeamByTeamIdAndUserId(model.teamId, model.aspnetuserId);
                if (teamCheck.Count() > 0)
                {
                    teamId = 0;
                }
                else
                {
                    Team data = new Team
                    {
                        Id = 0,                        
                        areaId = model.areaId,
                        memberName = model.memberName,
                        teamCode = model.teamCode,
                        isActive = 1,
                        moduleId=2,
                        teamId =model.teamId,
                        aspnetuserId = model.aspnetuserId
                    };

                    teamId = await teamService.SaveTeamNew(data);
                }

                return Json(teamId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamInfoByTeamId(int Id)
        {
            return Json(await teamService.GetTeamInfoByTeamId(Id));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTeamById(int Id)
        {
            await teamService.DeleteTeamById(Id);
            return Json(true);
        }

        #endregion
    }
}