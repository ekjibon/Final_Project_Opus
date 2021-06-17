using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class TrainingImplementationController : Controller
    {
        private readonly LangGenerate<TrainingImplementationLn> _lang;
        private readonly ITypeService typeService;
        private readonly ITrainingNewService trainingNewService;
        private readonly IEnrollTraineeService enrollTraineeService;
        private readonly IResourcePersonService resourcePersonService;
        private readonly ITrainingResourcePersonService trainingResourcePersonService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrainingImplementationController(IHostingEnvironment hostingEnvironment, ITrainingResourcePersonService trainingResourcePersonService, IResourcePersonService resourcePersonService, ITypeService typeService, ITrainingNewService trainingNewService, IEnrollTraineeService enrollTraineeService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<TrainingImplementationLn>(hostingEnvironment.ContentRootPath);
            this.typeService = typeService;
            this.trainingNewService = trainingNewService;
            this.enrollTraineeService = enrollTraineeService;
            this.resourcePersonService = resourcePersonService;
            this.trainingResourcePersonService = trainingResourcePersonService;
            _userManager = userManager;
        }

        // GET: TrainingImplementation
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.TrainingPlanId = id;
            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatus(2)
            };
            return View(model);
        }

        public async Task<IActionResult> Foreign(int id)
        {
            ViewBag.TrainingPlanId = id;
            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatus(2)
            };
            return View(model);
        }

        // POST: TrainingImplementation/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingImplementationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]);
                model.trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(model.id);
                return View(model);
            }

            TrainingInfoNew data = new TrainingInfoNew
            {
                Id = model.id,
                startDateActual = model.startDateActual,
                endDateActual = model.endDateActual,
                amountActual = model.actualExpence,
                noOfParticipantsActual = model.noOfParticipantsActual,
                status = 2,
            };

            await trainingNewService.UpdateTrainingInfoNewById(data);

            if(model.trainingType == 1)
            {
                return RedirectToAction(nameof(TrainingList));
            }
            else
            {
                return RedirectToAction(nameof(ForeignTrainingList));
            }
        }

        // GET: TrainingImplementation
        public async Task<IActionResult> TrainingList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByType(1, org)
            };
            return View(model);
        }

        public async Task<IActionResult> ForeignTrainingList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByType(2, org)
            };
            return View(model);
        }

        // GET: TrainingListForEvaluation
        public async Task<IActionResult> TrainingListForEvaluation()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatusandType(2,1,org)
            };
            return View(model);
        }

        public async Task<IActionResult> ForeignTrainingListForEvaluation()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainingImplementationViewModel model = new TrainingImplementationViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatusandType(2,2,org)
            };
            return View(model);
        }

        // GET: TrainingImplementation
        public async Task<IActionResult> Enroll(int id)
        {
            ViewBag.TrainingPlanId = id;
            TrainingEnrollViewModel model = new TrainingEnrollViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(id),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatus(2)
            };
            return View(model);
        }

        // POST: TrainingImplementation/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll([FromForm] TrainingEnrollViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]);
                model.trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(model.id);
                model.enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(model.id);
                return View(model);
            }

            //return Json(model);
            Nullable<int> id = null;
            if (model.employeeId == 0)
            {
                id = null;
            }
            else
            {
                id = model.employeeId;
            }

            EnrolledTrainee data = new EnrolledTrainee
            {
                employeeId = id,
                designation = model.designation,
                name = model.employeeName,
                workingPlace = model.organization,
                address = model.address,
                mobile = model.mobile,
                email = model.email,
                trainingInfoNewId = model.id,
            };

            await enrollTraineeService.SaveEnrolledTrainee(data);

            return RedirectToAction("Enroll", "TrainingImplementation", new
            {
                model.id
            });
        }

        // GET: TrainingImplementation
        public async Task<IActionResult> AssignResourcePerson(int id)
        {
            ViewBag.TrainingPlanId = id;
            TrainingEnrollViewModel model = new TrainingEnrollViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(id),
                trainingInfoNews = await trainingNewService.GetTrainingInfoNewByStatus(2),
                resourcePeople = await resourcePersonService.GetResourcePersonInfo(),
                trainingResourcePersons = await trainingResourcePersonService.GetTrainingResourcePersonByTrainingId(id)
            };
            return View(model);
        }

        // POST: TrainingImplementation/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignResourcePerson([FromForm] TrainingEnrollViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]);
                model.trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(model.id);
                model.enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(model.id);
                model.resourcePeople = await resourcePersonService.GetResourcePersonInfo();
                return View(model);
            }

            //return Json(model);

            TrainingResourcePerson data = new TrainingResourcePerson
            {
                trainingInfoNewId = model.id,
                resourcePersonId = Int32.Parse(model.resourcePersonId)
            };

            await trainingResourcePersonService.SaveTrainingResourcePerson(data);

            return RedirectToAction("AssignResourcePerson", "TrainingImplementation", new
            {
                model.id
            });
        }


        public async Task<IActionResult> DetailsView(int id)
        {
            TrainingEnrollViewModel model = new TrainingEnrollViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingImplementationEN.json", "TrainingNew/TrainingImplementationBN.json", Request.Cookies["lang"]),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(id),
                trainingResourcePersons = await trainingResourcePersonService.GetTrainingResourcePersonByTrainingId(id)
            };
            return View(model);
        }
    }
}