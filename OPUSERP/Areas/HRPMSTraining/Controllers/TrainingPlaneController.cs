using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSTraining.Models;
using OPUSERP.Areas.HRPMSTraining.Models.Lang;
using OPUSERP.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSTraining.Controllers
{
    [Area("HRPMSTraining")]
    public class TrainingPlaneController : Controller
    {
        private readonly LangGenerate<TrainingPlaneLn> _lang;

        public TrainingPlaneController(IHostingEnvironment hostingEnvironment)
        {
            _lang = new LangGenerate<TrainingPlaneLn>(hostingEnvironment.ContentRootPath);
        }

        // GET: TrainingPlane
        public IActionResult Index()
        {
            TrainingPlaneViewModel model = new TrainingPlaneViewModel
            {
                fLang =_lang.PerseLang("TrainingNew/TrainingPlaneEN.json", "TrainingNew/TrainingPlaneBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // POST: TrainingPlane/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TrainingPlaneViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //TrainingName data = new TrainingName
            //{
            //    Id = Int32.Parse(model.awardId),
            //    awardName = model.awardName,
            //    awardNameBn = model.awardNameBn,
            //    awardShortName = model.awardShortName
            //};

            //await bookAwardService.SaveAwardInfo(data);

            return RedirectToAction(nameof(Index));
        }

        //xxxxxxxxxxxxxx
    }
}