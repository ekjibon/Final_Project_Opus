using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.CLUB.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class TrainingController : Controller
    {
        private readonly LangGenerate<TrainingLn> _lang;
        private readonly IAddressService addressService;
        private readonly ITrainingService trainingService;
        private readonly ITraningHistoryService traningHistoryService;
        private readonly IPersonalInfoService personalInfoService;

        public TrainingController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAddressService addressService, ITrainingService trainingService, ITraningHistoryService traningHistoryService)
        {
            _lang = new LangGenerate<TrainingLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
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

            MemberTraningLog data = new MemberTraningLog
            {
                Id = model.trainingLogID,
                employeeId = model.employeeID,
                fromDate = model.fromDate,
                toDate = model.toDate,
                countryId = Int32.Parse(model.country),
                trainingCategoryId = Int32.Parse(model.category),
                trainingInstituteId = Int32.Parse(model.institute),
                trainingTitle = model.trainingTitle,
                remarks = model.remarks,
                //sponsoringAgency = model.sponsoringAgency
            };

            await traningHistoryService.SaveTraningHistory(data);

            return RedirectToAction(nameof(Index));
        }

        // Delete: Children
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