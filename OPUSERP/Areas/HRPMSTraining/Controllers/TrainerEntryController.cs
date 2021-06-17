using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSTraining.Models;
using OPUSERP.Areas.HRPMSTraining.Models.Lang;
using OPUSERP.Helpers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTraining.Controllers
{
    [Area("HRPMSTraining")]
    public class TrainerEntryController : Controller
    {
        private readonly LangGenerate<TrainerEntryLn> _lang;

        public TrainerEntryController(IHostingEnvironment hostingEnvironment)
        {
            _lang = new LangGenerate<TrainerEntryLn>(hostingEnvironment.ContentRootPath);
        }

        // GET: TrainerEntry
        public IActionResult Index()
        {
            TrainerEntryViewModel model = new TrainerEntryViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainingNameEN.json", "TrainingNew/TrainingNameBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }

        // POST: TrainerEntry/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] TrainerEntryViewModel model)
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