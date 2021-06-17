using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.CRM.Services.Budget.Interfaces;
using OPUSERP.ERPService.MasterData;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class TeamBudgetController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly ICompanyBudgetService companyBudgetService;
        public TeamBudgetController(ITeamService teamService, IYearCourseTitleService yearCourseTitleService, ICompanyBudgetService companyBudgetService)
        {
            this.teamService = teamService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.companyBudgetService = companyBudgetService;
            
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            TeamBudgetViewModel model = new TeamBudgetViewModel()
            {
                years = await yearCourseTitleService.GetYear()
               
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetYearWiseBudget(int yearId)
        {
            return Json(await companyBudgetService.GetcompanyBudgetsByyearId(yearId));
        }
        [HttpGet]
        public async Task<IActionResult> GetTealList()
        {
            var data = await teamService.GetAllTeam();
            return Json(data.Where(x=>x.teamId==null && x.moduleId==2));
        }
    }
}
