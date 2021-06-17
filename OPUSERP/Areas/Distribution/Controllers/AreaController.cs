using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComTypeViewModel = OPUSERP.Areas.CRMMasterData.Models.ComTypeViewModel;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("Distribution")]
    public class AreaController : Controller
    {
        private readonly ICommunicationService communicationService;
        private readonly ICompanyGroupService companyGroupService;
        private readonly ISalesLevelService salesLevelService;
        private int Depth;
        public AreaController(ICommunicationService  communicationService, ISalesLevelService salesLevelService, ICompanyGroupService companyGroupService)
        {
            this.communicationService = communicationService;
            this.companyGroupService = companyGroupService;
            this.salesLevelService = salesLevelService;
            Depth = 0;
        }

        public async Task<IActionResult> CommunicationType()
        {
            ComTypeViewModel model = new ComTypeViewModel
            {
                communicationTypes = await communicationService.GetAllCommunicationType()
            };
            return View(model);
        }

        // POST: Organization Status/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommunicationType([FromForm] ComTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.communicationTypes= await communicationService.GetAllCommunicationType();
                return View(model);
            }

            CommunicationType data = new CommunicationType
            {
                Id = model.comTypeId,
                communicationTypeName = model.communicationTypeName,
            };

            await communicationService.SaveCommunicationType(data);

            return RedirectToAction(nameof(CommunicationType));
        }

        public async Task<IActionResult> Area()
        {
            AreaViewModel model = new AreaViewModel
            {
                areas = await communicationService.GetAllArea(),
                salesLevels=await salesLevelService.GetAllSalesLevel()
            };
            return View(model);
        }

        // POST: Organization Status/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Area([FromForm] AreaViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.areas = await communicationService.GetAllArea();
            //    return View(model);
            //}
            if (model.masterareaid == 0)
            {
                model.masterareaid = null;
            }
            Area data = new Area
            {
                Id = model.areaId,
                areaCode = model.areaCode,
                areaName = model.areaName,
                areaLocation = model.areaLocation,
                areaId=model.masterareaid
               
            };

            await communicationService.SaveArea(data);

            return RedirectToAction(nameof(Area));
        }

        public async Task<IActionResult> DeleteCommunicationType(int id)
        {
            await communicationService.DeleteCommunicationTypeById(id);
            return RedirectToAction("Communication", "CommunicationType", new { Area = "CRMMasterData" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            await communicationService.DeleteAreaById(id);
            return RedirectToAction(nameof(Area));
        }
        [Route("/api/global/getsaleslevelbyId/{Id}")]
        public async Task<JsonResult> getsaleslevelbyId(int Id)
        {
            var photo = await salesLevelService.GetSalesLevelById(Id);
            return Json(photo);
        }
        //public async Task<IActionResult> CompanyGroup()
        //{
        //    CompanyGroupViewModel model = new CompanyGroupViewModel
        //    {
        //        companyGroups = await companyGroupService.GetAllCompanyGroup()
        //    };
        //    return View(model);
        //}

        //// POST: Company Group/Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CompanyGroup([FromForm] CompanyGroupViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.companyGroups = await companyGroupService.GetAllCompanyGroup();
        //        return View(model);
        //    }

        //    CompanyGroup data = new CompanyGroup
        //    {
        //        Id = model.companyGroupId,
        //        groupName = model.compGroupName,
        //        createdBy = HttpContext.User.Identity.Name,
        //        createdAt = DateTime.Now
        //    };

        //    await companyGroupService.SaveCompanyGroup(data);

        //    return RedirectToAction(nameof(CompanyGroup));
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMenusJson()
        {
            Depth = 2;
            string s = "[{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", 0, "Start", "Start", "null", "parrent", await this.GenerateTree(0, "Start", 0)) + "}]";

            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }

        //Recursion For Retriving Tree 
        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            Depth = Math.Max(level, Depth);
            string data = "";
            IEnumerable<Area> MenuData = await communicationService.GetMenusByParrentId(parrentid);

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (Area menu in MenuData)
            {
                string type = "parrent";

               // if (menu.IsLast) { type = "last"; }

                string child = await GenerateTree(menu.Id, menu.areaName, level + 1);

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.areaName, menu.areaName, parrentid, menu.salesLevelId, child) + "}";

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