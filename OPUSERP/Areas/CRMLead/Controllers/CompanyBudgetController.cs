using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.CRM.Services.Budget.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMLead")]
    public class CompanyBudgetController : Controller
    {
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ICompanyBudgetService companyBudgetService;
        private readonly IUserInfoes userInfo;
        public CompanyBudgetController(IYearCourseTitleService yearCourseTitleService, IERPCompanyService eRPCompanyService, ICompanyBudgetService companyBudgetService, IUserInfoes userInfo)
        {
            this.yearCourseTitleService = yearCourseTitleService;
            this.eRPCompanyService = eRPCompanyService;
            this.companyBudgetService = companyBudgetService;
            this.userInfo = userInfo;
        }

        public async Task<IActionResult> Index()
        {
            CompanyBudgetViewModel model = new CompanyBudgetViewModel()
            {
                years = await yearCourseTitleService.GetYear(),
                companies = await eRPCompanyService.GetAllCompany(),
                companyBudgets = await companyBudgetService.GetAllcompanyBudgets()
            };
            return View(model);
        }

        // POST: ActivityCategory/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CompanyBudgetViewModel model)
        {
            var company = await eRPCompanyService.GetAllCompany();
            var chkYear = await companyBudgetService.GetcompanyBudgetsByyearId(Int32.Parse(model.ratingYearId));
            bool flag = false;
            if (chkYear.FirstOrDefault()?.yearId == Int32.Parse((model.ratingYearId)))
            {
                ModelState.AddModelError(string.Empty, "This year already exists. Choose another rating year!!!");
                flag = true;
            }

            if (!ModelState.IsValid || flag)
            {
                model.years = await yearCourseTitleService.GetYear();
                model.companyBudgets = await companyBudgetService.GetAllcompanyBudgets();
                return View(model);
            }

            CompanyBudgets data = new CompanyBudgets
            {
                Id = model.Id,
                yearId = Convert.ToInt32(model.ratingYearId),
                companyId = company.OrderByDescending(a => a.Id).FirstOrDefault().Id,
                initialAmount = model.initialAmount,
                surveillence = model.surveillanceAmount,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await companyBudgetService.SaveCompanyBudget(data);

            return RedirectToAction(nameof(Index));
        }
    }
}