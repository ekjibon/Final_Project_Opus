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
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTrainingNew.Controllers
{
    [Authorize]
    [Area("HRPMSTrainingNew")]
    public class TrainerInfoController : Controller
    {
        private readonly LangGenerate<TrainerInfoLn> _lang;
        private readonly IResourcePersonService resourcePersonService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrainerInfoController(IHostingEnvironment hostingEnvironment, IResourcePersonService resourcePersonService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<TrainerInfoLn>(hostingEnvironment.ContentRootPath);
            this.resourcePersonService = resourcePersonService;
            _userManager = userManager;
        }

        // GET: TrainerInfo
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            TrainerInfoViewModel model = new TrainerInfoViewModel
            {
                fLang = _lang.PerseLang("TrainingNew/TrainerInfoEN.json", "TrainingNew/TrainerInfoBN.json", Request.Cookies["lang"]),
                resourcePeople = await resourcePersonService.GetResourcePersonInfoByOrg(org),
            };
            return View(model);
        }

        // POST: TrainerInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainerInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("TrainingNew/TrainerInfoEN.json", "TrainingNew/TrainerInfoBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string org = user.org;

            ResourcePerson data = new ResourcePerson
            {
                Id = model.trainerId,
                name = model.name,
                designation = model.designation,
                workPlace = model.workPlace,
                specialization = model.Specialization,
                contactNumber = model.contactNumber,
                performance = model.Performance,
                remarks = model.Remarks,
                orgType = org,
                email = model.email
            };

            await resourcePersonService.SaveResourcePersonInfo(data);

            return RedirectToAction(nameof(Index));
        }


    }
}