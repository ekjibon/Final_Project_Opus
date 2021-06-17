using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class AdjustmentController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalService approvalService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public AdjustmentController(ISalaryService salaryService, IPersonalInfoService personalInfoService, IERPCompanyService eRPCompanyService, IUserInfoes userInfo, IApprovalService approvalService, IHostingEnvironment hostingEnvironment, IConverter converter)
        {
            this.salaryService = salaryService;
            this.personalInfoService = personalInfoService;
            this.eRPCompanyService = eRPCompanyService;
            this.userInfo = userInfo;
            this.approvalService = approvalService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Leave Without Pay

        public async Task<ActionResult> LeaveWithoutPay()
        {
            LeaveWithoutPayViewModel model = new LeaveWithoutPayViewModel
            {
                leaveWithoutPays = await salaryService.GetAllLeaveWithoutPay(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveWithoutPay([FromForm] LeaveWithoutPayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.leaveWithoutPays = await salaryService.GetAllLeaveWithoutPay();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                return View(model);
            }

            LeaveWithoutPay data = new LeaveWithoutPay
            {
                Id = model.leaveWithoutPayId,
                employeeInfoId = model.employeeInfoId,
                salaryPeriodId = model.salaryPeriodId,
                noOfDays = model.noOfDays
            };

            await salaryService.SaveLeaveWithoutPay(data);
            return RedirectToAction(nameof(LeaveWithoutPay));
        }

        #endregion

        #region Advance Adjustment

        public async Task<ActionResult> AdvanceAdjustment()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            AdvanceAdjustmentViewModel model = new AdvanceAdjustmentViewModel
            {
                advanceAdjustments = await salaryService.GetAllAdvanceAdjustment(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes"),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        public async Task<ActionResult> EditAdvanceAdjustment(int id)
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            AdvanceAdjustmentViewModel model = new AdvanceAdjustmentViewModel
            {
                advanceAdjustmentDetails = await salaryService.GetAllAdvanceAdjustmentDetailByMasterId(id),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes")
            };

            return View(model);
        }

        public async Task<ActionResult> ReportAdvanceAdjustment(int id)
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            AdvanceAdjustmentViewModel model = new AdvanceAdjustmentViewModel
            {
                advanceAdjustmentDetails = await salaryService.GetAllAdvanceAdjustmentDetailByMasterId(id),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes"),
                companies = await eRPCompanyService.GetAllCompany(),
            };

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult ReportAdvanceAdjustmentPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
            url = scheme + "://" + host + "/Payroll/Adjustment/ReportAdvanceAdjustment/" + id;


            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdvanceAdjustment([FromForm] AdvanceAdjustmentViewModel model)
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            if (!ModelState.IsValid)
            {
                model.advanceAdjustments = await salaryService.GetAllAdvanceAdjustment();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes");
                return View(model);
            }

            AdvanceAdjustment data = new AdvanceAdjustment
            {
                Id = model.advanceAdjustmentId,
                employeeInfoId = model.employeeInfoId,
                salaryHeadId = model.salaryHeadId,
                advanceAmount = model.advanceAmount,
                installmentAmount = model.installmentAmount,
                noOfInstallment = model.noOfInstallment,
                date = model.date,
                isContinue = model.isContinue,
                isComplete = model.isComplete,
                purpose = model.purpose
            };

            int masterId = await salaryService.SaveAdvanceAdjustment(data);

            decimal? total = model.advanceAmount;
            DateTime date = Convert.ToDateTime(model.date);
            for (int i = 0; i < model.noOfInstallment; i++)
            {
                decimal? amount = 0;
                if (total <= model.installmentAmount)
                {
                    amount = total;
                }
                else
                {
                    amount = model.installmentAmount;
                }

                AdvanceAdjustmentDetail loanScheduleDetail = new AdvanceAdjustmentDetail
                {
                    advanceAdjustmentId = masterId,
                    scheduleAmount = amount,
                    scheduleDate = date,
                    isPaid = 0,
                };
                await salaryService.SaveAdvanceAdjustmentDetail(loanScheduleDetail);
                total = total - amount;
                date = date.AddMonths(1);
            }


            return RedirectToAction(nameof(AdvanceAdjustment));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdvanceAdjustment([FromForm] LoanScheduleDetailsViewModel model)
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            if (!ModelState.IsValid)
            {
                model.loanScheduleDetails = await salaryService.GetAllLoanScheduleDetailByMasterId(model.Id);
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes");
                return View(model);
            }

            //return Json(model);
            if (model.selectPaymentHeadIds != null)
            {
                for (int i = 0; i < model.selectPaymentHeadIds.Count(); i++)
                {
                    salaryService.UpdateAdvanceAdjustmentDetail((int)model.selectPaymentHeadIds[i], model.selectPaymentHeadAmounts[i]);
                }
            }

            return RedirectToAction("EditAdvanceAdjustment", "Adjustment", new
            {
                id = model.Id,
            });
        }

        #endregion

        #region Loan Schedule

        public async Task<ActionResult> LoanSchedule()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            LoanScheduleViewModel model = new LoanScheduleViewModel
            {
                loanScheduleMasters = await salaryService.GetAllLoanScheduleMaster(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                loanPolicies = await salaryService.GetAllLoanPolicy()
            };

            return View(model);
        }

        public async Task<ActionResult> EditLoanSchedule(int id)
        {            
            LoanScheduleViewModel model = new LoanScheduleViewModel
            {
                loanScheduleDetails = await salaryService.GetAllLoanScheduleDetailByMasterId(id),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                loanPolicies = await salaryService.GetAllLoanPolicy()
            };

            return View(model);
        }

        public async Task<ActionResult> ReportLoanSchedule(int id)
        {            
            LoanScheduleViewModel model = new LoanScheduleViewModel
            {
                loanScheduleDetails = await salaryService.GetAllLoanScheduleDetailByMasterId(id),
                loanScheduleReportViewModels = await salaryService.GetLoanReportById(id),
                //salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                //loanPolicies = await salaryService.GetAllLoanPolicy(),
                companies = await eRPCompanyService.GetAllCompany(),
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ReportLoanSchedulePdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;
            url = scheme + "://" + host + "/Payroll/Adjustment/ReportLoanSchedule/" + id;


            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoanSchedule([FromForm] LoanScheduleViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.loanScheduleMasters = await salaryService.GetAllLoanScheduleMaster();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.loanPolicies = await salaryService.GetAllLoanPolicy();
                return View(model);
            }

            LoanScheduleMaster data = new LoanScheduleMaster
            {
                Id = model.advanceAdjustmentId,
                employeeInfoId = model.employeeInfoId,
                loanPolicyId = model.loanPolicyId,
                totalLoanAmount = model.advanceAmount,
                loanDate = model.date,
                installmentAmount = model.installmentAmount,
                noOfInstallment = model.noOfInstallment,
                isContinue = model.isContinue,
                isComplete = model.isComplete,
                purpose = model.purpose,
                interestRate = model.interestRate,
                interestAmount = model.interestAmount,
                TotalLoanWithInterestAmount = model.totalAmountWithInterest,
                approveStatus = 0
            };

            int masterId = await salaryService.SaveLoanScheduleMaster(data);
            decimal? total = model.totalAmountWithInterest;
            DateTime date = Convert.ToDateTime(model.date);
            for (int i = 0; i < model.noOfInstallment; i++)
            {
                decimal? amount = 0;
                if (total <= model.installmentAmount)
                {
                    amount = total;
                }
                else
                {
                    amount = model.installmentAmount;
                }

                LoanScheduleDetail loanScheduleDetail = new LoanScheduleDetail
                {
                    loanScheduleMasterId = masterId,
                    scheduleAmount = amount,
                    scheduleDate = date,
                    isPaid = 0,
                };
                await salaryService.SaveLoanScheduleDetail(loanScheduleDetail);
                total = total - amount;
                date = date.AddMonths(1);
            }

            IEnumerable<ApprovalDetail> approvalDetails = await approvalService.GetApprovalDetailByEmpAndType(model.employeeInfoId, "Loan");

            var j = 1;
            var Active = 1;
            foreach (ApprovalDetail supervisor in approvalDetails)
            {
                LoanRoute loanRoute = new LoanRoute
                {
                    loanScheduleMasterId = masterId,
                    employeeId = supervisor.approverId,
                    routeOrder = j,
                    isActive = Active,
                };
                await salaryService.SaveLoanRoute(loanRoute);
                j++;
                Active = 0;
            }

            return RedirectToAction(nameof(LoanSchedule));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLoanSchedule([FromForm] LoanScheduleDetailsViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                model.loanScheduleDetails = await salaryService.GetAllLoanScheduleDetailByMasterId(model.Id);
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.loanPolicies = await salaryService.GetAllLoanPolicy();
                return View(model);
            }

            //return Json(model);
            if (model.selectPaymentHeadIds != null)
            {
                for (int i = 0; i < model.selectPaymentHeadIds.Count(); i++)
                {
                    salaryService.UpdateLoanScheduleDetail((int)model.selectPaymentHeadIds[i], model.selectPaymentHeadAmounts[i]);
                }
            }

            return RedirectToAction("EditLoanSchedule", "Adjustment", new
            {
                id = model.Id,
            });
        }

        #endregion

        #region Loan Schedule Individual

        public async Task<ActionResult> LoanScheduleIndividual()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            //ViewBag.employId = empId;
           
            LoanScheduleViewModel model = new LoanScheduleViewModel
            {
                loanScheduleMasters = await salaryService.GetAllLoanScheduleMasterByEmpId(empId),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                loanPolicies = await salaryService.GetAllLoanPolicy(),
                approvalDetail = await approvalService.GetSingleApprovalDetailByEmpAndType(empId, "Loan"),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoByCode()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);

            return Json(EmpInfo);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoanPolicyById(int id)
        {            
            var data = await salaryService.GetLoanPolicyById(id);
            return Json(data);
        }

        #endregion

        #region Loan Approve

        public async Task<IActionResult> LoanApprove()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }
            LoanScheduleViewModel model = new LoanScheduleViewModel
            {
                loanRoutes = await salaryService.GetLoanRouteByEmpId(empId),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoanApproveAll([FromForm] LoanScheduleViewModel model)
        {
            int Status = 1;
            if (model.registerids != null)
            {
                for (int i = 0; i < model.registerids.Count(); i++)
                {
                    LoanRoute leaveRoute = await salaryService.GetLoanRouteById((int)model.ids[i]);
                    int? nextOrder = leaveRoute.routeOrder + 1;
                    await salaryService.UpdateLoanRoute(leaveRoute.Id, 0);

                    LoanRoute leaveRouteNew = await salaryService.GetLoanRouteByRouteOrder((int)leaveRoute.loanScheduleMasterId, (int)nextOrder);

                    if (leaveRouteNew != null)
                    {
                        var leaveFutureEmployee = await personalInfoService.GetEmployeeInfoById((int)leaveRouteNew?.employeeId);
                        await salaryService.UpdateLoanRoute(leaveRouteNew.Id, 1);
                        await salaryService.UpdateLoanScheduleMasterApproval((int)model.registerids[i], Status);
                    }
                    else
                    {
                        await salaryService.UpdateLoanScheduleMasterApproval((int)model.registerids[0], Status);
                    }
                }
            }

            return RedirectToAction(nameof(LoanApprove));
        }


        #endregion

        #region Employee Arrear

        public async Task<ActionResult> EmployeeArrear()
        {
            EmployeeArrearViewModel model = new EmployeeArrearViewModel
            {
                employeeArrears = await salaryService.GetAllEmployeeArrear(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeArrear([FromForm] EmployeeArrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.employeeArrears = await salaryService.GetAllEmployeeArrear();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                return View(model);
            }
            var arrear = await salaryService.GetEmployeeArrearByEmpAndPeriodId(model.employeeInfoId, model.salaryPeriodId);
            if (arrear.Count() > 0)
            {
                await salaryService.DeleteEmployeeArrearByEmpAndPeriodId(model.employeeInfoId, model.salaryPeriodId);
            }

            IEnumerable<EmployeeArrear> lstArrear = await salaryService.EmployeeArrearCalculationByEmpId(model.employeeInfoId, model.salaryPeriodId, model.arrearAmount, model.txtBonus);

            foreach (var item in lstArrear)
            {
                EmployeeArrear data = new EmployeeArrear
                {
                    Id = model.employeeArrearId,
                    employeeInfoId = model.employeeInfoId,
                    salaryPeriodId = model.salaryPeriodId,
                    salaryHeadId = item.salaryHeadId,
                    arrearAmount = item.arrearAmount,
                    ratio = item.ratio
                };
                await salaryService.SaveEmployeeArrear(data);
            }
            return RedirectToAction(nameof(EmployeeArrear));
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeArrearCalculation(int empId, int periodId, decimal? totalAmount, decimal? bonusAmount)
        {
            return Json(await salaryService.EmployeeArrearCalculationByEmpId(empId, periodId, totalAmount, bonusAmount));
        }

        #endregion

        #region Lfa Info

        public async Task<ActionResult> LfaInfo()
        {
            LfaInfoViewModel model = new LfaInfoViewModel
            {
                lfaInfos = await salaryService.GetAllLfaInfo(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LfaInfo([FromForm] LfaInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.lfaInfos = await salaryService.GetAllLfaInfo();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                return View(model);
            }

            LfaInfo data = new LfaInfo
            {
                Id = model.lfaInfoId,
                employeeInfoId = model.employeeInfoId,
                salaryPeriodId = model.salaryPeriodId,
                lfaAmount = model.lfaAmount,
                lfaStartDate = model.lfaStartDate,
                lfaEndDate = model.lfaEndDate,
                currentLfaYearNo = model.currentLfaYearNo,
                lfaStatus = "No"

            };

            await salaryService.SaveLfaInfo(data);
            return RedirectToAction(nameof(LfaInfo));
        }

        [HttpGet]
        public async Task<IActionResult> GetLastYearBasic(int empId, int periodId)
        {
            return Json(await salaryService.GetLastYearBasic(empId, periodId));
        }

        [HttpGet]
        public async Task<IActionResult> GetLfaInfoByEmpId(int empId)
        {
            return Json(await salaryService.GetLfaInfoByEmpId(empId));
        }

        #endregion

        #region Monthly Allowance

        public async Task<ActionResult> MonthlyAllowance()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            MonthlyAllowanceViewModel model = new MonthlyAllowanceViewModel
            {
                monthlyAllowances = await salaryService.GetAllMonthlyAllowance(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                salaryHeads = salaryHead.Where(a => a.isMonthlyAllowance == "Yes")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MonthlyAllowance([FromForm] MonthlyAllowanceViewModel model)
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            if (!ModelState.IsValid)
            {
                model.monthlyAllowances = await salaryService.GetAllMonthlyAllowance();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes");
                return View(model);
            }

            MonthlyAllowance data = new MonthlyAllowance
            {
                Id = model.monthlyAllowanceId,
                employeeInfoId = model.employeeInfoId,
                salaryPeriodId = model.salaryPeriodId,
                salaryHeadId = model.salaryHeadId,
                allowanceAmount = model.allowanceAmount,
                remarks = model.remarks
            };

            await salaryService.SaveMonthlyAllowance(data);
            return RedirectToAction(nameof(MonthlyAllowance));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await salaryService.DeleteMonthlyAllowanceById(id);
            return RedirectToAction(nameof(MonthlyAllowance));
            //return RedirectToAction("Index", "Award", new
            //{
            //    id = empId
            //});
        }

        [HttpGet]
        public async Task<IActionResult> GetMealChargeByPeriod(int salaryPeriodId)
        {
            return Json(await salaryService.GetMealChargeByPeriod(salaryPeriodId));
        }
        #endregion

        #region Attendance Summary

        //public async Task<ActionResult> AttendanceSummary(int? Id)
        public async Task<ActionResult> AttendanceSummary()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            AttendanceSummaryViewModel model = new AttendanceSummaryViewModel
            {
                attendanceSummaries = await salaryService.GetAttendanceSummary(),
                salaryPeriods = await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            //ViewBag.empAttendanceSummaryId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttendanceSummary([FromForm] AttendanceSummaryViewModel model)
        {
            var dupCheck = await salaryService.GetDuplicateAttendanceSummary(model.empAttendanceSummaryId, model.salaryPeriodId, model.employeeInfoId);
            if (!ModelState.IsValid || dupCheck.Count() > 0)
            {
                model.attendanceSummaries = await salaryService.GetAttendanceSummary();
                model.salaryPeriods = await salaryService.GetAllSalaryPeriod();
                model.visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData();
                if (dupCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate employee data under the same period !!!");
                }
                return View(model);
            }

            EmpAttendanceSummary data = new EmpAttendanceSummary
            {
                Id = model.empAttendanceSummaryId,
                employeeInfoId = model.employeeInfoId,
                salaryPeriodId = model.salaryPeriodId,
                weeklyOff = model.weeklyOff,
                holiday = model.holiday,
                leave = model.leave,
                present = model.present,
                absent = model.absent,
                late = model.late,
                remarks = model.remarks
            };
            int Id = await salaryService.SaveAttendanceSummary(data);
            //return RedirectToAction(nameof(AttendanceSummary), new { Id });
            return RedirectToAction(nameof(AttendanceSummary));
        }

        public async Task<IActionResult> DeleteAttendaneSummary(int id)
        {
            await salaryService.DeleteAttendanceSummaryById(id);
            return RedirectToAction(nameof(AttendanceSummary));
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendanceSummaryByPeriod(int? id, int? salaryPeriodId)
        {
            return Json(await salaryService.GetAttendanceSummaryByPeriod(id, salaryPeriodId));
        }
        #endregion

    }
}