using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.Distribution.Controllers
{
    [Authorize]
    [Area("Distribution")]
    public class SalesTeamDeploymentController : Controller
    {
        // GET: /<controller>/
        private readonly ISalesLevelService salesLevelService;
        private readonly ISalesTeamDeploymentService salesTeamDeploymentService;
        private readonly ICommunicationService communicationService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private int Depth;
        public SalesTeamDeploymentController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, ICommunicationService communicationService, ISalesTeamDeploymentService salesTeamDeploymentService, ISalesLevelService salesLevelService)
        {
            
            this.salesLevelService = salesLevelService;
            this.communicationService = communicationService;
            this.salesTeamDeploymentService = salesTeamDeploymentService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            Depth = 0;
        }
        public async Task<IActionResult> Index()
        {
            SalesTeamDeploymentViewModel model = new SalesTeamDeploymentViewModel
            {
               
               
                salesLevels = await salesLevelService.GetAllSalesLevel(),
                salesTeamDeployments=await salesTeamDeploymentService.GetAllSalesTeamDeployment(),
                areas=await communicationService.GetAllArea()

            };
            return View(model);
        }
        // POST: Degree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SalesTeamDeploymentViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {

                model.salesLevels = await salesLevelService.GetAllSalesLevel();
                model.salesTeamDeployments = await salesTeamDeploymentService.GetAllSalesTeamDeployment();
                return View(model);
            }

            SalesTeamDeployment data = new SalesTeamDeployment
            {
                Id = (int)model.Id,
                areaId = (int)model.areaId,
                salesLevelId = (int)model.salesLevelId,
                employeeInfoId = (int)model.employeeInfoId,
                salesTeamDeploymentId=(int)model.salesTeamDeploymentId
                


            };

            await salesTeamDeploymentService.SaveSalesTeamDeployment(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await salesTeamDeploymentService.DeleteSalesTeamDeploymentById(id);
            return RedirectToAction(nameof(Index));
        }

        [Route("/api/global/getEmployeeInfo/")]
        public async Task<JsonResult> getEmployeeInfo()
        {
            var emplist = await personalInfoService.GetEmployeeInfo();
            return Json(emplist);
        }
        [Route("/api/global/getEmployeeInfoinDep/")]
        public async Task<JsonResult> getEmployeeInfoinDep()
        {
            var emplist = await personalInfoService.GetEmployeeInfo();
            var empdeplist = await salesTeamDeploymentService.GetAllSalesTeamDeployment();
            List<int?> lstempid = empdeplist.Select(x => x.employeeInfoId).ToList();
            emplist = emplist.Where(x => lstempid.Contains(x.Id));
            
            return Json(emplist);
        }

        [Route("/api/global/getareabysaleslevelId/{Id}")]
        public async Task<JsonResult> getareabysaleslevelId(int Id)
        {
            var arealist = await communicationService.GetAllAreabysaleslevelid(Id);
            return Json(arealist);
        }
        [Route("/api/global/getsaleslevelparentId/{Id}")]
        public async Task<JsonResult> getsaleslevelparentId(int Id)
        {
            var arealist = await salesLevelService.GetAllSalesLevelbyparentId(Id);
            return Json(arealist);
        }
        [Route("/api/global/getemployeepicbyId/{Id}")]
        public async Task<JsonResult> getemployeepicbyId(int Id)
        {
            var photo = await photographService.GetPhotographByEmpIdAndType(Id,"Profile");
            return Json(photo);
        }

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
            IEnumerable<SalesTeamDeployment> MenuData = await salesTeamDeploymentService.GetSalesTeamDeploymentByparrentId(parrentid);

            if (MenuData.Count() <= 0)
            {
                return data;
            }

            int last = MenuData.Last().Id;
            foreach (SalesTeamDeployment menu in MenuData)
            {
                string type = "parrent";
               
                // if (menu.IsLast) { type = "last"; }
                string child = await GenerateTree(menu.Id, menu.employeeInfo.nameEnglish, level + 1);

                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameEN\":\"{2}\",\"parent\":\"{3}\",\"type\":\"{4}\",\"children\":[{5}]", menu.Id, menu.employeeInfo.nameEnglish, menu.employeeInfo.nameEnglish, parrentid, menu.salesLevelId, child) + "}";

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
