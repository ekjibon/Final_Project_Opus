using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSTrainingNew.Models;
using OPUSERP.Areas.HRPMSTrainingNew.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTrainingNew.Controllers
{
    [Authorize]
    [Area("HRPMSTrainingNew")]
    public class TrainingPlanningController : Controller
    {
        private readonly LangGenerate<TrainingPlanningLn> _lang;
        private readonly ITypeService typeService;
        private readonly ITrainingNewService trainingNewService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrainingPlanningController(IHostingEnvironment hostingEnvironment, IAddressService addressService, IYearCourseTitleService yearCourseTitleService, ITypeService typeService, ITrainingNewService trainingNewService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<TrainingPlanningLn>(hostingEnvironment.ContentRootPath);
            this.typeService = typeService;
            this.trainingNewService = trainingNewService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.addressService = addressService;
            _userManager = userManager;
        }

        // GET: TrainingPlanning
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            //return Json(org);

            TrainingPlanningViewModel model = new TrainingPlanningViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByType(1, org),
                courseTitles = await yearCourseTitleService.GetCourseTitle(),
                years = await yearCourseTitleService.GetYear()
            };
            return View(model);
        }

        public async Task<IActionResult> Foreign()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainingPlanningViewModel model = new TrainingPlanningViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByType(2, org),
                courseTitles = await yearCourseTitleService.GetCourseTitle(),
                years = await yearCourseTitleService.GetYear(),
                countries = await addressService.GetAllContry(),
            };
            return View(model);
        }

        // POST: TrainingPlanning/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingPlanningViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]);
                model.trainingInfoNews = await trainingNewService.GetTrainingInfoNew();
                return View(model);
            }

            //return Json(model);

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            var arr = await typeService.GetTypNamesByIds(model.employeeTypeMultiple);

            TrainingInfoNew data = new TrainingInfoNew
            {
                Id = Convert.ToInt32(model.planningId),
                course = model.course,
                courseObjective = model.courseObjective,
                amount = model.amount,
                startDate = model.planeStartDate,
                endDate = model.planeEndDate,
                year = model.year,
                noOfParticipants = model.participant,
                employeeTypeId = model.employeeType,
                budget = model.budget,
                remarks = model.remark,
                trainingType = model.trainingType,
                countryId = model.country,
                location = model.location,
                orgType = org,
                status = 1,
                employeeTypeNames = String.Join(", ",arr),
                employeeTypes = String.Join(",",model.employeeTypeMultiple)
            };

            await trainingNewService.SaveTrainingInfoNew(data);

            if(model.trainingType == 1)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Foreign));
            }
            
        }

        //xxxxxxxxxxxxxx 
    }
}