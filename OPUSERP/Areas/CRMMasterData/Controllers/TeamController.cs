using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private int Depth;
        public TeamController(ITeamService teamService)
        {
            this.teamService =teamService;
            Depth = 0;
        }

        public async Task<IActionResult> Team()
        {
            TeamViewModel model = new TeamViewModel()
            {
                teams = await teamService.GetAllTeamByModule(2)
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
                return View(model);
            }

            Team data = new Team
            {
                Id = model.steamId,
                memberName = model.MemberName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await teamService.SaveTeam(data);

            return RedirectToAction(nameof(Team));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamParent([FromForm] TeamViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.sectors = await sectorService.GetAllSector();
            //    return View(model);
            //}

            Team data = new Team
            {
                Id = model.ssteamId,
                memberName = model.childMemberName,
                teamId=model.teamId,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await teamService.SaveTeam(data);

            return RedirectToAction(nameof(Team));
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