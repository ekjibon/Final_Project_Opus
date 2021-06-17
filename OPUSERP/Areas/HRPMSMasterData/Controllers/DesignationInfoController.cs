using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class DesignationInfoController : Controller
    {
        private readonly LangGenerate<DesignationLn> _lang;
        // GET: DesignationInfo
        private readonly IDesignationDepartmentService designationDepartmentService;

        public DesignationInfoController(IHostingEnvironment hostingEnvironment, IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<DesignationLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
        }
        // GET: DesignationInfo
        public async Task<IActionResult> Index()
        {
            var plan = await designationDepartmentService.GetDesignations();
            string desiCode = (plan.Count() + 1).ToString();
            DesignationInfoViewModel model = new DesignationInfoViewModel
            {
                fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]),
                designations = await designationDepartmentService.GetDesignations()
            };
            ViewBag.desiCode = desiCode;
            return View(model);
        }

        // POST: DesignationInfo/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DesignationInfoViewModel model)
        {
            var plan = await designationDepartmentService.GetDesignations();
            string desiCode = (plan.Count() + 1).ToString();
            //string productionNo = ("Expense/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.designationId > 0)
            {
                desiCode = model.designationCode;
            }
          
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]);
                model.designations = await designationDepartmentService.GetDesignations();
                return View(model);
            }

            Designation data = new Designation
            {
                //empType = model.empType,
                Id = model.designationId,
                designationCode = desiCode,
                designationName = model.designationName,
                designationNameBN = model.designationNameBn,
                shortName = model.shortName
            };

            await designationDepartmentService.SaveDesignation(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        //Get All Departmets
        [Route("global/api/Departments")]
        [HttpGet]
        public async Task<IActionResult> Department()
        {
            return Json(await designationDepartmentService.GetDepartment());
        }
        #endregion

    }
}