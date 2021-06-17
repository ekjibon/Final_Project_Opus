using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class LanguageController : Controller
    {
        private readonly LangGenerate<LanguageLn> _lang;
        // GET: Language
        private readonly IMembershipLanguageService membershipLanguageService;

        public LanguageController(IHostingEnvironment hostingEnvironment, IMembershipLanguageService membershipLanguageService)
        {
            _lang = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            this.membershipLanguageService = membershipLanguageService;
        }
        // GET: Language
        public async Task<IActionResult> Index()
        {
            LanguageViewModel model = new LanguageViewModel
            {
                fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                languages = await membershipLanguageService.GetLanguageInfo()
            };
            return View(model);
        }

        // POST: Language/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] LanguageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]);
                model.languages = await membershipLanguageService.GetLanguageInfo();
                return View(model);
            }

            Language data = new Language
            {
                Id = model.languageId,
                languageName = model.languageName,
                languageNameBn = model.languageNameBn,
                languageShortName = model.languageShortName
            };

            await membershipLanguageService.SaveLanguageInfo(data);

            return RedirectToAction(nameof(Index));
        }

    }
}