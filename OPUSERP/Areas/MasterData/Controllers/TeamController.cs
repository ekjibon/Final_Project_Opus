using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.MasterData;
using Newtonsoft.Json.Linq;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.Interfaces;

namespace OPUSERP.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IUserInfoes userInfoes;
        private readonly IModuleAssignService moduleAssignService;
        private int Depth;

        public TeamController(ITeamService teamService, IUserInfoes userInfoes, IModuleAssignService moduleAssignService)
        {
            this.teamService = teamService;
            this.userInfoes = userInfoes;
            this.moduleAssignService = moduleAssignService;
            Depth = 0;
        }

        public async Task<IActionResult> Team()
        {
            TeamViewModel model = new TeamViewModel()
            {
                teams = await teamService.GetAllTeam(),
                aspNetUsers= await userInfoes.GetUserInfoByModule(2),
                eRPModules = await moduleAssignService.GetERPModulesForTeam(),
            };
            return View(model);
        }

        // POST: Sector/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Team([FromForm] TeamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.teams = await teamService.GetAllTeamByModule(2);
                model.aspNetUsers = await userInfoes.GetUserInfoByModule(2);
                return View(model);
            }

            Team data = new Team
            {
                Id = Convert.ToInt32(model.tId),
                memberName = model.memberName,
                teamCode = model.teamCode,
                teamId = model.parentId,
                aspnetuserId = model.aspnetuserId,
                isActive = 1,
                moduleId = model.moduleId

            };

            await teamService.SaveTeam(data);
            return RedirectToAction(nameof(Team));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamParent([FromForm] TeamViewModel model)
        {            
            Team data = new Team
            {
                Id = model.ssteamId,
                memberName = model.childMemberName,
                teamCode = model.parentTeamCode,
                isActive = 1,
                moduleId = model.teamModuleId,
                teamId = model.teamId,
                aspnetuserId = model.aspnetuserIdChild,
            };

            await teamService.SaveTeam(data);
            return RedirectToAction(nameof(Team));
        }

        public async Task<IActionResult> DeleteTeamById(int id)
        {
            await teamService.DeleteTeamById(id);
            return RedirectToAction("Team", "Team", new { Area = "MasterData" });
        }

        [HttpGet]
        [Route("/api/global/GetAspNetUserByModule/{moduleId}")]
        public async Task<JsonResult> GetAspNetUserByModule(int moduleId)
        {
            var result = await userInfoes.GetUserInfoByModule(moduleId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamsJson(int org)
        {
            Depth = 4;
            string s, tm;
            int Id = await teamService.GetRootId(org);

            tm = await this.GenerateTree(Id, "Start", 0);
            Team tempData = await teamService.GetTeamById(Id);

            //if (tm == "")
            s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", Id, tempData.memberName, tempData.memberName, "null", 23, tm) + "}";
            //else s = tm;

            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }

        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            int isHead = 2;
            Depth = Math.Max(level, Depth);
            string data = "";

            IEnumerable<Team> sectors = await teamService.GetTeamByParrentId(parrentid);

            if (sectors.Count() <= 0) return data;
            int last = sectors.Last().Id;

            foreach (Team menu in sectors)
            {
                string child = await GenerateTree(menu.Id, menu.memberName, level + 1);
                string name = menu.memberName;

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", menu.Id, name, menu.memberName, parrentid, isHead, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }
            return data;
        }


    }
}