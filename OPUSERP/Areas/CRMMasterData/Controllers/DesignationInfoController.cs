//using UMBRELLA.Areas.MasterData.Models.Lang;

//using UMBRELLA.Helpers;

using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
//using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;

using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class DesignationInfoController : Controller
    {
        //private readonly LangGenerate<DesignationLn> _lang;
        // GET: DesignationInfo
        private readonly ICRMDesignationDepartmentService designationDepartmentService;

        public DesignationInfoController(ICRMDesignationDepartmentService designationDepartmentService)
        {
            //_lang = new LangGenerate<DesignationLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
        }
        // GET: DesignationInfo
        public async Task<IActionResult> Index()
        {
            DesignationViewModel model = new DesignationViewModel
            {
                //fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]),
                designations = await designationDepartmentService.GetDesignations()
            };
            return View(model);
        }

        // POST: DesignationInfo/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DesignationViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    //model.fLang = _lang.PerseLang("MasterData/DesignationEN.json", "MasterData/DesignationBN.json", Request.Cookies["lang"]);
            //    model.designations = await designationDepartmentService.GetDesignations();
            //    return View(model);
            //}

            CRMDesignation data = new CRMDesignation
            {
                //empType = model.empType,
                Id = model.designationId,
                designationCode = model.designationCode,
                designationName = model.designationName,
                designationNameBN = model.designationNameBn,
                shortName = model.shortName
            };

            await designationDepartmentService.SaveDesignation(data);

            return RedirectToAction(nameof(Index));
        }

        #region API Section
        //Get All Departmets
        [Route("global/api/CRMDepartments")]
        [HttpGet]
        public async Task<IActionResult> CRMDepartment()
        {
            return Json(await designationDepartmentService.GetDepartment());
        }
        #endregion

    }
}