using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class TrainingController : Controller
    {
        private readonly LangGenerate<TrainingLn> _lang;
        private readonly IAddressService addressService;
        private readonly ITrainingService trainingService;
        private readonly IPhotographService photographService;
        private readonly ITraningHistoryService traningHistoryService;
        private readonly IPersonalInfoService personalInfoService;

        public TrainingController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IPersonalInfoService personalInfoService, IAddressService addressService, ITrainingService trainingService, ITraningHistoryService traningHistoryService)
        {
            _lang = new LangGenerate<TrainingLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
            this.photographService = photographService;
            this.trainingService = trainingService;
            this.traningHistoryService = traningHistoryService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Training
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TrainingViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                fLang = _lang.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry(),
                trainingCategories = await trainingService.GetTrainingCategories(),
                trainingInstitutes = await trainingService.GetTrainingInstitute(),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            //return Json(model);
            return View(model);
        }

        // POST: Training/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            TraningLog data = new TraningLog
            {
                Id = model.trainingLogID,
                employeeId = model.employeeID,
                fromDate = model.fromDate,
                toDate = model.toDate,
               // countryId = Int32.Parse(model.country),
                trainingCategoryId = Int32.Parse(model.category),
                trainingInstituteId = Int32.Parse(model.institute),
                trainingTitle = model.trainingTitle,
                remarks = model.remarks,
                //sponsoringAgency = model.sponsoringAgency
            };

            await traningHistoryService.SaveTraningHistory(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "Training", new
            {
                id = model.employeeID
            });
        }

        // Delete: Language
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await traningHistoryService.DeleteTraningHistoryById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Training", new
            {
                id = empId
            });
        }

        #region API Section
        [Route("global/api/trainingLogById/{id}")]
        [HttpGet]
        public async Task<IActionResult> trainingLogById(int id)
        {
            return Json(await traningHistoryService.GetTraningHistoryByEmpId(id));
        }
        #endregion

    }
}