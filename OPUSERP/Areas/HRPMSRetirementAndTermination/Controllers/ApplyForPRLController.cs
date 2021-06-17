using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSRetirementAndTermination.Models;
using OPUSERP.Areas.HRPMSRetirementAndTermination.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.RetirementAndTermination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.HRPMS.Services.RetirementAndTermination.Interface;

namespace OPUSERP.Areas.HRPMSRetirementAndTermination.Controllers
{
    [Authorize]
    [Area("HRPMSRetirementAndTermination")]
    public class ApplyForPRLController : Controller
    {
        private readonly LangGenerate<PRLApplicationLn> _lang;
        private readonly IPRLEntryService pRLEntryService;
        private readonly IResignInformationService resignInformationService;

        public ApplyForPRLController(IHostingEnvironment hostingEnvironment, IResignInformationService resignInformationService, IPRLEntryService pRLEntryService)
        {
            this.resignInformationService = resignInformationService;
            _lang = new LangGenerate<PRLApplicationLn>(hostingEnvironment.ContentRootPath);
            this.pRLEntryService = pRLEntryService;
        }

        public IActionResult Index()
        {
            var model = new PRLApplicationViewModel()
            {
                //flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json")
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"])
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PRLApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {

                //model.flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json");
                model.flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            PRLApplication data = new PRLApplication
            {
                applicationDate = model.applicationDate,
                employeeId = Convert.ToInt32(model.employeeId),
                applicationName = model.applicationName,
                status = "Pending"
            };

            await pRLEntryService.SavePrlEntry(data);

            return RedirectToAction(nameof(ApprovePRLApplication));
        }

        public async Task<IActionResult> PRLApplicationList()
        {
            var model = new PRLApplicationViewModel()
            {
                //flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json"),
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]),
                pRLApplications = await pRLEntryService.GetPRLEntryByStatus("Approved")
            };
            return View(model);
        }

        public async Task<IActionResult> ApprovePRLApplication()
        {
            var model = new PRLApplicationViewModel()
            {
                //flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json"),
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]),
                pRLApplications = await pRLEntryService.GetPRLEntryByStatus("Pending")
            };
            return View(model);
        }

        public async Task<IActionResult> Approve([FromForm] PRLApplication model)
        {
            await pRLEntryService.UpdatePRLStatus(model.Id, model.fromDate, model.toDate, model.duration, model.status);

            return RedirectToAction(nameof(ApprovePRLApplication));

        }

        public async Task<IActionResult> ApplicationDetails(int id)
        {
            var model = new PRLApplicationViewModel()
            {
                //flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json"),
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]),
                pRLApplicationsById = await pRLEntryService.GetPrlEntryById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> ApproveApplicationDetails(int id)
        {
            var model = new PRLApplicationViewModel()
            {
                //flang = _lang.PerseLang("RetirementTermination/PRLApplicationBN.json"),
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]),
                pRLApplicationsById = await pRLEntryService.GetPrlEntryById(id)
            };
            return View(model);
        }

        public async Task<IActionResult> ApplyResign()
        {
            var model = new PRLApplicationViewModel()
            {
                flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]),
                resignInformation = await resignInformationService.GetResignInformation(),
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyResign([FromForm] PRLApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.flang = _lang.PerseLang("RetirementTermination/PRLApplicationEN.json", "RetirementTermination/PRLApplicationBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            ResignInformation data = new ResignInformation
            {
                Id = model.resignId ?? 0,
                employeeId = Convert.ToInt32(model.employeeId),
                resignDate = model.resignDate,
                lastWorkingDate = model.lastWorkingDate,
                resignReason = model.resignReason,
            };

            await resignInformationService.SaveResignInformation(data);

            return RedirectToAction(nameof(ApplyResign));
        }

    }
}