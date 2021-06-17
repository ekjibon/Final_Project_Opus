using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSACR.Models;
using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.ACR.Interfaces;
using OPUSERP.HRPMS.Services.Assignment.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.PunishmentCharge.Interfaces;
using OPUSERP.HRPMS.Services.RewardInfo.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSACR.Controllers
{
    [Area("HRPMSACR")]
    public class ACRController : Controller
    {
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IACRService aCRService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IAssignmentService assignmentService;
        private readonly ICPService cPService;
        private readonly IPromotionService promotionService;
        private readonly IRewardService rewardService;

        private readonly LangGenerate<ACRLn> _lang;

        public ACRController(IHostingEnvironment hostingEnvironment, IDesignationDepartmentService designationDepartmentService,IACRService aCRService, IPersonalInfoService personalInfoService
            , IAssignmentService assignmentService, ICPService cPService, IPromotionService promotionService, IRewardService rewardService)
        {
            _lang = new LangGenerate<ACRLn>(hostingEnvironment.ContentRootPath);
            this.designationDepartmentService = designationDepartmentService;
            this.aCRService = aCRService;
            this.personalInfoService = personalInfoService;
            this.assignmentService = assignmentService;
            this.cPService = cPService;
            this.promotionService = promotionService;
            this.rewardService = rewardService;
        }

        // GET: ACR
        public async Task<IActionResult> Index()
        {
            var model = new ACRAuthorityViewModel
            {
                fLang = _lang.PerseLang("ACR/ACRAuthoritySetupEN.json", "ACR/ACRAuthoritySetupBN.json", Request.Cookies["lang"]),
                designations = await designationDepartmentService.GetDesignations(),
                aCRAuthorities=await aCRService.GetAllACRAuthority()
            };
            return View(model);
        }
        // POST: ACR/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ACRAuthorityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.designations = await designationDepartmentService.GetDesignations();
                model.aCRAuthorities = await aCRService.GetAllACRAuthority();

                return View(model);
            }

            ACRAuthority data = new ACRAuthority
            {
                empDesignationName=model.empDesignationName,
                CADesignationName=model.CADesignationName,
                CSADesignationName=model.CSADesignationName
            };

            int lstId = await aCRService.SaveACRAuthority(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ACRReport(int empId)
        {
            var model = new EmployeeWithDesignationVM
            {
                assignments = await assignmentService.GetAssignmentByEmpId(empId),
                charges = await cPService.GetChargeByEmpId(empId),
                punishments = await cPService.GetPunishmentByEmpId(empId),
                rewards=await rewardService.GetRewardByEmpId(empId),
                promotions=await promotionService.GetPromotionInfoByEmpId(empId)

            };
            return PartialView(model);
        }

        public async Task<IActionResult> ACRDetailsEntryForm()
        {
            var model = new EmployeeWithDesignationVM
            {
                //fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json"),
                employeeWithDesignations = await personalInfoService.GetEmployeeInfoDetailsList(0)
            };
            return View(model);
        }

        // GET: ACR/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ACR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACR/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ACR/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ACR/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ACR/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ACR/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}