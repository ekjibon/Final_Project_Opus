using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSTrainingNew.Models;
using OPUSERP.Areas.HRPMSTrainingNew.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSTrainingNew.Controllers
{
    [Authorize]
    [Area("HRPMSTrainingNew")]
    public class EvalutionPlaceController : Controller
    {
        private readonly LangGenerate<TrainingPlanningLn> _lang;
        private readonly ITypeService typeService;
        private readonly ITrainingNewService trainingNewService;
        private readonly IEnrollTraineeService enrollTraineeService;

        public EvalutionPlaceController(IHostingEnvironment hostingEnvironment, IEnrollTraineeService enrollTraineeService, ITypeService typeService, ITrainingNewService trainingNewService)
        {
            _lang = new LangGenerate<TrainingPlanningLn>(hostingEnvironment.ContentRootPath);
            this.typeService = typeService;
            this.trainingNewService = trainingNewService;
            this.enrollTraineeService = enrollTraineeService;
        }

        // GET: EvalutionPlace
        public async Task<IActionResult> Index(int id)
        {
            EvalutionPlaceViewModel model = new EvalutionPlaceViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(id),
            };
            return View(model);
        }

        // GET: EvalutionPlace/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] EvalutionPlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]);
                model.trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(Convert.ToInt32(model.planningId));
                model.enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(Convert.ToInt32(model.planningId));
                return View(model);
            }

            //return Json(model);

            for (int i = 0; i < model.placeId.Length; i++)
            {
                EnrolledTrainee data = new EnrolledTrainee
                {
                    Id = model.placeId[i],
                    positon = model.place[i],
                    completionStatus = model.completionStatus[i],
                    remarks = model.remark[i]
                };

                await enrollTraineeService.UpdatePositionEnrolledTrainee(data);
            }      

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detils(int id)
        {
            EvalutionPlaceViewModel model = new EvalutionPlaceViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]),
                employeeTypes = await typeService.GetAllEmployeeType(),
                trainingInfoNew = await trainingNewService.GetTrainingInfoNewById(id),
                enrolledTrainees = await enrollTraineeService.GetEnrolledTraineeByPlannId(id),
            };
            return View(model);
        }

        //xxxxxxxxxxxxxx
    }
}