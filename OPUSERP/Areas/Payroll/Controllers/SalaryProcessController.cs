using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class SalaryProcessController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IERPCompanyService eRPCompanyService;
        public SalaryProcessController(ISalaryService salaryService, IERPCompanyService eRPCompanyService)
        {
            this.salaryService = salaryService;
            this.eRPCompanyService = eRPCompanyService;
        }

        #region Salary Process

        public async Task<ActionResult> Index()
        {
            var companies = await eRPCompanyService.GetAllCompany();
            ViewBag.Company = companies.FirstOrDefault().companyName;

            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }        

        [HttpPost]
        public async Task<JsonResult> EMPSalaryProcess([FromForm] SalaryProcessViewModel model)
        {
            try
            {
                var count = 1;
                if (model.employeeInfoId != null)
                {
                     await salaryService.EmpSalaryProcess(Convert.ToInt32(model.salaryPeriodId), Convert.ToInt32(model.employeeInfoId));
                }
                else
                {
                    var masterId = await salaryService.EmpSalaryProcess(Convert.ToInt32(model.salaryPeriodId), 0);
                    count = masterId.FirstOrDefault().employeeInfoId;
                }

                var remote = HttpContext.Connection.RemoteIpAddress;
                var local = HttpContext.Connection.LocalIpAddress;
                string userip = remote.ToString();
                string useripLocal = local.ToString();

                SalaryProcessLog data = new SalaryProcessLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    processType = "Salary",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryProcessLog(data);

                SalaryStatusLog logdata = new SalaryStatusLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    statusType = "Salary Process",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryStatusLog(logdata);

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> EMPSalaryReProcess([FromForm] SalaryProcessViewModel model)
        {
            try
            {
                var count = 1;
                if (model.employeeInfoId != null)
                {
                    await salaryService.EmpSalaryProcess(Convert.ToInt32(model.salaryPeriodId), Convert.ToInt32(model.employeeInfoId));
                }
                else
                {
                    var masterId = await salaryService.EmpSalaryProcess(Convert.ToInt32(model.salaryPeriodId), 0);
                    count = masterId.FirstOrDefault().employeeInfoId;
                }

                var remote = HttpContext.Connection.RemoteIpAddress;
                var local = HttpContext.Connection.LocalIpAddress;
                string userip = remote.ToString();
                string useripLocal = local.ToString();

                SalaryProcessLog data = new SalaryProcessLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    processType = "Salary",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryProcessLog(data);

                SalaryStatusLog logdata = new SalaryStatusLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodId,
                    statusType = "Salary ReProcess",
                    ipAddress = userip
                };
                await salaryService.SaveSalaryStatusLog(logdata);

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditSalaryPeriodForlockLabel(int Id, int lockLabel)
        {
            await salaryService.EditSalaryPeriodForlockLabel(Id, lockLabel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlySalaryReportByPeriodId(int salaryPeriodId)
        {
            var data = await salaryService.GetMonthlySalaryReportByPeriodId(salaryPeriodId, 0, 0);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> SaveSalaryComments([FromForm] ProcessSalaryRemarksViewModel model)
        { 
            try
            {
                await salaryService.DeleteProcessSalaryRemarks(model.empId, model.salperId);
                ProcessSalaryRemarks data = new ProcessSalaryRemarks
                {
                    Id = 0,
                    employeeInfoId = model.empId,
                    salaryPeriodId = model.salperId,
                    comments = model.comments,
                    proposedAmount=model.proposedAmount
                };
                int DocumentId = await salaryService.SaveProcessSalaryRemarks(data);
                return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProcessSalaryRemarks(int? empId, int? salperId)
        {
            await salaryService.DeleteProcessSalaryRemarks(empId, salperId);
            return Json(true);
        }        

        [HttpPost]
        public async Task<JsonResult> SaveSalaryStatusLog([FromForm] SalaryStatusLogViewModel model)
        {
            try
            {
                var local = HttpContext.Connection.LocalIpAddress;                
                string useripLocal = local.ToString();

                SalaryStatusLog data = new SalaryStatusLog
                {
                    Id = 0,
                    salaryPeriodId = model.salaryPeriodLoadId,                   
                    statusType = model.statusType,
                    remarks=model.remarks,
                    ipAddress= useripLocal
                };
                int DocumentId = await salaryService.SaveSalaryStatusLog(data);
                await salaryService.EditSalaryPeriodForlockLabel(Convert.ToInt32(model.salaryPeriodLoadId), Convert.ToInt32(model.draftFinalId));

                return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Process Log List

        public async Task<ActionResult> SalaryProcessLog()
        {            
            ProcessSalaryRemarksViewModel model = new ProcessSalaryRemarksViewModel
            {
                salaryProcessLogs = await salaryService.GetAllSalaryProcessLog()
            };
            return View(model);
        }

        #endregion

        #region Status Log List

        public async Task<ActionResult> SalaryStatusLog()
        {
            SalaryStatusLogViewModel model = new SalaryStatusLogViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSalaryStatusLogByPeriodId(int salaryPeriodId)
        {
            var data = await salaryService.GetSalaryStatusLogByPeriodId(salaryPeriodId);
            return Json(data);
        }

        #endregion

        #region Salary Ongoing List

        public async Task<ActionResult> SalaryOngoing()
        {
            var companies = await eRPCompanyService.GetAllCompany();
            ViewBag.Company = companies.FirstOrDefault().companyName;

            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        #endregion

        #region Salary Approve List

        public async Task<ActionResult> SalaryApprove()
        {
            var companies = await eRPCompanyService.GetAllCompany();
            ViewBag.Company = companies.FirstOrDefault().companyName;

            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        #endregion

        #region Salary Disburse List

        public async Task<ActionResult> SalaryDisburse()
        {
            var companies = await eRPCompanyService.GetAllCompany();
            ViewBag.Company = companies.FirstOrDefault().companyName;

            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        #endregion

        #region Period Lock
        public async Task<ActionResult> SalarySheetLock()
        {
            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()

            };

            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> EditSalaryPeriodForSalarySheetLock(int Id, int lockLabel)
        {
            await salaryService.SetSalaryPeriodLock(Id, lockLabel,HttpContext.User.Identity.Name);
            return RedirectToAction("SalarySheetLock");

        }

        #endregion

        #region Period UnLock
        public async Task<ActionResult> SalarySheetUnLock()
        {
            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> SalaryPeriodUnLock(int Id, int lockLabel)
        {
            await salaryService.SetSalaryPeriodLock(Id, lockLabel, HttpContext.User.Identity.Name);
            return RedirectToAction("SalarySheetUnLock");

        }

        #endregion

        #region Wages Process

        public async Task<ActionResult> WagesIndex()
        {
            SalaryProcessViewModel model = new SalaryProcessViewModel
            {
                salaryPeriods = await salaryService.GetAllSalaryPeriod()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> WagesSalaryProcess([FromForm] SalaryProcessViewModel model)
        {
            try
            {
                var count = 1;
                if (model.employeeInfoId != null)
                {
                    await salaryService.WagesSalaryProcess(Convert.ToInt32(model.salaryPeriodId), Convert.ToInt32(model.employeeInfoId));
                }
                else
                {
                    var masterId = await salaryService.WagesSalaryProcess(Convert.ToInt32(model.salaryPeriodId), 0);
                    count = masterId.Count();
                }

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region API


        #endregion
    }
}