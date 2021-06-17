using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class BonusProcessController : Controller
    {
        private readonly ISalaryService salaryService;

        public BonusProcessController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        #region Bonus Process

        public async Task<ActionResult> Index()
        {
            BonusProcessViewModel model = new BonusProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = await salaryService.GetAllSalaryHead(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> EmployeesBonusProcess([FromForm] BonusProcessViewModel model)
        {
            try
            {
                var count = 1;
                string user = HttpContext.User.Identity.Name;
                if (model.employeeInfoId != null)
                {
                    await salaryService.EmpBonusProcess(model.salaryPeriodId, model.salaryHeadId, Convert.ToInt32(model.employeeInfoId), model.lastDayofPeriod, user, model.bonusFor);
                }
                else
                {
                    var masterId = await salaryService.EmpBonusProcess(model.salaryPeriodId, model.salaryHeadId, 0, model.lastDayofPeriod, user, model.bonusFor);
                    count = masterId.FirstOrDefault().employeeInfoId;
                }

                #region Log
                var remote = HttpContext.Connection.RemoteIpAddress;
                string userip = remote.ToString();

                SalaryProcessLog data = new SalaryProcessLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    processType = "Bonus",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryProcessLog(data);

                SalaryStatusLog logdata = new SalaryStatusLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    statusType = "Bonus Process",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryStatusLog(logdata);
                #endregion

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> ProcessEmpSalaryMasterBySp(int? salperId, int? empId)
        {
            await salaryService.ProcessEmpSalaryMasterBySp(salperId, empId);
            return Json(true);
        }

        #endregion

        #region Bonus Ongoing List

        public async Task<ActionResult> BonusOngoing()
        {
            BonusProcessViewModel model = new BonusProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = await salaryService.GetAllSalaryHead()
            };
            return View(model);
        }

        #endregion

        #region Bonus Approve List

        public async Task<ActionResult> BonusApprove()
        {
            BonusProcessViewModel model = new BonusProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        #endregion

        #region Bonus Disburse List

        public async Task<ActionResult> BonusDisburse()
        {
            BonusProcessViewModel model = new BonusProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        #endregion

        #region Bonus Period Lock

        public async Task<ActionResult> BonusPeriodLock()
        {
            BonusProcessViewModel model = new BonusProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };

            return View(model);
        }
        #endregion

        #region Bonus Log List

        public async Task<ActionResult> BonusStatusLog()
        {
            SalaryStatusLogViewModel model = new SalaryStatusLogViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }       

        #endregion

    }
}