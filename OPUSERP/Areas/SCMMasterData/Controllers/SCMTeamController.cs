using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class SCMTeamController : Controller
    {
        private readonly ISCMTeamService sCMTeamService;
        private readonly IUserInfoes userInfoes;

        public SCMTeamController(ISCMTeamService sCMTeamService, IUserInfoes userInfoes)
        {
            this.sCMTeamService = sCMTeamService;
            this.userInfoes = userInfoes;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            TeamMasterViewModel model = new TeamMasterViewModel
            {
                teamMasters=await sCMTeamService.GetAllTeamMaster(),
                aspNetUsersViews= await userInfoes.GetUserInfo()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] TeamMasterViewModel model)
        {
            try
            {
                TeamMaster master = new TeamMaster
                {
                    Id = Convert.ToInt32(model.teamMasterId),
                    teamCode = model.teamCode,
                    teamName = model.teamName,
                    leaderId=model.leaderId,
                    isActive = 1
                };
                await sCMTeamService.SaveTeamMaster(master);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> TeamMember()
        {
           TeamMemberViewModel model = new TeamMemberViewModel
           {
                aspNetUsersViews = await userInfoes.GetUserInfo(),
                teamMasters=await sCMTeamService.GetAllTeamMaster()
           };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TeamMember([FromForm] TeamMasterViewModel model)
        {
            try
            {
                TeamMember master = new TeamMember
                {
                    Id = Convert.ToInt32(model.teamMemberId),
                    teamMasterId= Convert.ToInt32(model.teamMasterId),
                    memberId = model.memberId,
                    isActive = 1
                };
                await sCMTeamService.SaveTeamMember(master);
                return RedirectToAction(nameof(TeamMember));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Route("api/SCMTeam/GetTeamMember/{masterId}")]
        public async Task<JsonResult> GetTeamMember(int masterId)
        {
            var result =await sCMTeamService.GetAllTeamMemberByMasterId(masterId);
            return Json(result);
        }
    }
}