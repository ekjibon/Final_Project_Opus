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
    public class TrainingNameController : Controller
    {
        private readonly LangGenerate<TrainingNameLn> _lang;

        public TrainingNameController(IHostingEnvironment hostingEnvironment)
        {
            _lang = new LangGenerate<TrainingNameLn>(hostingEnvironment.ContentRootPath);
        }

        // GET: TrainingName
        public IActionResult Index()
        {
            TrainingNameViewModel model = new TrainingNameViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingNameEN.json", "TrainingNew/TrainingNameBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // POST: TrainingName/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TrainingNameViewModel model)
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