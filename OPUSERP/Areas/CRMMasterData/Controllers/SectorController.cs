using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class SectorController : Controller
    {
        private readonly ISectorService sectorService;
        private int Depth;
        public SectorController(ISectorService sectorService)
        {
            this.sectorService = sectorService;
            Depth = 0;
        }

        public async Task<IActionResult> Sector()
        {
            SectorViewModel model = new SectorViewModel()
            {
                sectors = await sectorService.GetAllSector()
            };
            return View(model);
        }

        // POST: Sector/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sector([FromForm] SectorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.sectors = await sectorService.GetAllSector();
                return View(model);
            }

            Sector data = new Sector
            {
                Id = Convert.ToInt32(model.sId),
                sectorId = model.ssectorId,
                sectorName = model.sectorName
            };

            await sectorService.SaveSector(data);
            return RedirectToAction(nameof(Sector));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectorParent([FromForm] SectorViewModel model)
        {          
            Sector data = new Sector
            {
                Id =Convert.ToInt32(model.sssectorId),
                sectorName = model.childSectorName,
                sectorId = model.ParrentId
            };

            await sectorService.SaveSector(data);
            return RedirectToAction(nameof(Sector));
        }

        
        public async Task<IActionResult> DeleteSectorById(int id)
        {
            try
            {
                await sectorService.DeleteSectorById(id);
                return RedirectToAction("Sector", "Sector", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("Sector", "Sector", new { Area = "CRMMasterData" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetSectorsJson(int org)
        {
            Depth = 4;
            string s, tm;
            int Id = await sectorService.GetRootId(org); 

            tm = await this.GenerateTree(Id, "Start", 0);
            Sector tempData = await sectorService.GetSectorById(Id);
           
            //if (tm == "")
                s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", Id, tempData.sectorName, tempData.sectorName, "null", 23, tm) + "}";
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
           
            IEnumerable<Sector> sectors = await sectorService.GetSectorByParrentId(parrentid);

            if (sectors.Count() <= 0) return data;
            int last = sectors.Last().Id;

            foreach (Sector menu in sectors)
            {
                string child = await GenerateTree(menu.Id, menu.sectorName, level + 1);
                string name = menu.sectorName;
               
                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", menu.Id, name, menu.sectorName, parrentid, isHead, child) + "}";

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