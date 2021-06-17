using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Controllers
{
    [Authorize]
    [Area("HRPMSReport")]
    public class TrainingReportController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEnrollTraineeService enrollTraineeService;

        public TrainingReportController(IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, IEnrollTraineeService enrollTraineeService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _userManager = userManager;
            this.enrollTraineeService = enrollTraineeService;
        }
               
        // GET: TrainingReport
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ReportingViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),                
            };
            return View(model);
        }

        #region API

        [AllowAnonymous]
        [Route("global/api/getTrainingListByEmployeeId/{employeeId}")]
        [HttpGet]
        public async Task<IActionResult> GetTrainingListByEmployeeId(int employeeId)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await enrollTraineeService.GetTrainingListByEmployeeId(employeeId));
        }

        #endregion

        //xxxxxxxxxxxxx
    }
}