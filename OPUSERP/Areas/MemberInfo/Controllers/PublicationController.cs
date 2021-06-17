using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.CLUB.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class PublicationController : Controller
    {
        private readonly LangGenerate<Publication> _lang;
        private readonly IAwardPublicationLanguageService awardPublicationService;
        private readonly IPersonalInfoService personalInfoService;

        public PublicationController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IAwardPublicationLanguageService awardPublicationService)
        {
            _lang = new LangGenerate<Publication>(hostingEnvironment.ContentRootPath);
            this.awardPublicationService = awardPublicationService;
            this.personalInfoService = personalInfoService;
        }

        // GET: Publication
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new PublicationViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                publications = await awardPublicationService.GetPublicationsByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: Publication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PublicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.publications = await awardPublicationService.GetPublicationsByEmpId(Int32.Parse(model.employeeID));
                model.fLang = _lang.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            MemberPublication data = new MemberPublication
            {
                Id = Int32.Parse(model.publicationId),
                employeeId = Int32.Parse(model.employeeID),
                publicationName = model.publicationName,
                publicationNubmer = model.publicationNubmer,
                publicationType = model.publicationType,
                publicationPlace = model.publicationPlace,
                publicationDate = model.publicationDate

            };

            await awardPublicationService.SavePublication(data);

            return RedirectToAction(nameof(Index));
        }

    }
}