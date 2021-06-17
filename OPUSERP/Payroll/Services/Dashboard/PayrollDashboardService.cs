using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.Areas.Payroll.Models.Dashboard;
using OPUSERP.Areas.SCMDashboard.Models;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Dashboard.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Dashboard.Interfaces;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.Dashboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.Dashboard
{
    public class PayrollDashboardService : IPayrollDashboardService
    {
        private readonly ERPDbContext _context;

        public PayrollDashboardService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<PayrollDashboardcountViewModel> GetPayrollDataCountViewModel()
        {
            List<PayrollDashboardcountViewModel> models = new List<PayrollDashboardcountViewModel>();
            try
            {
                DateTime date = DateTime.Now;
                DateTime firstDay = new DateTime(date.Year, 1, 1);
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                int monthno = DateTime.Now.Month;
                DateTime premonth = DateTime.Now.AddMonths(monthno * -1);
                int year = DateTime.Now.Year - 7;
                List<int> years = new List<int>();

                List<DateTime> months = new List<DateTime>();
                List<string> monthshow = new List<string>();
                List<decimal> monthlySalary = new List<decimal>();

                List<string> departmentsname = new List<string>();
                List<decimal> departmentwiseSalary = new List<decimal>();

                List<string> designationsname = new List<string>();
                List<decimal> designationwiseSalary = new List<decimal>();

                List<decimal> deptotalamountbyyearone = new List<decimal>();
                List<decimal> deptotalamountbyyeartwo = new List<decimal>();
                List<decimal> deptotalamountbyyearthree = new List<decimal>();


                List<decimal> taxlist = new List<decimal>();
                List<decimal> pflist = new List<decimal>();
                List<decimal> yearlybonus = new List<decimal>();




                List<Department> departments = _context.departments.ToList();
                List<Designation> designations = _context.designations.ToList();
                List<string> empdesignations = _context.employeeInfos.Select(x=>x.designation).Distinct().ToList();
                List<ProcessEmpSalaryStructure> processEmpSalaryStructures = _context.ProcessEmpSalaryStructures.Include(x => x.salaryHead).Include(x => x.salaryPeriod.salaryYear).Include(x => x.salaryPeriod.salaryType).Include(x => x.employeeInfo.department).Include(x => x.employeeInfo.employeeType).ToList();
                int periodid = processEmpSalaryStructures.Where(x=>x.salaryPeriod.salaryType.salaryTypeName!="Bonus").Max(x => x.salaryPeriodId);
                decimal averagebasic = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary" && x.salaryPeriodId == periodid).Sum(x => x.amount) / processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary" && x.salaryPeriodId == periodid).Count();
                decimal heighestbasic = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary" && x.salaryPeriodId == periodid).Max(x => x.amount);
                decimal averagetotal = processEmpSalaryStructures.Where(x => x.salaryPeriodId == periodid&&x.salaryHead.salaryHeadType=="Addition").Sum(x => x.amount) / processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary" && x.salaryPeriodId == periodid).Count();
                decimal heighesttotal = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.salaryPeriodId == periodid).GroupBy(x=>x.employeeInfoId).Max(x => x.Sum(y=>y.amount));
                decimal totalprovisionsalarypercent = processEmpSalaryStructures.Where(x => x.salaryPeriodId == periodid && x.employeeInfo.employeeType.empType != "Permanent" && x.salaryHead.salaryHeadType == "Addition").Sum(x => x.amount) * 100 / processEmpSalaryStructures.Where(x => x.salaryPeriodId == periodid && x.salaryHead.salaryHeadType == "Addition").Sum(x => x.amount);
                decimal totalpermanentsalrypercent = processEmpSalaryStructures.Where(x => x.salaryPeriodId == periodid && x.employeeInfo.employeeType.empType == "Permanent" && x.salaryHead.salaryHeadType == "Addition").Sum(x => x.amount) * 100 / processEmpSalaryStructures.Where(x => x.salaryPeriodId == periodid && x.salaryHead.salaryHeadType == "Addition").Sum(x => x.amount);
                foreach (Department dep in departments)
                {
                    departmentsname.Add(dep.deptName);

                    decimal amount = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.employeeInfo.departmentId == dep.Id).Sum(x => x.amount);
                    departmentwiseSalary.Add(amount);

                    decimal amountfisrt = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.employeeInfo.departmentId == dep.Id && x.salaryPeriod.salaryYear.yearName== Convert.ToDateTime(DateTime.Now).Year.ToString()).Sum(x => x.amount);
                    deptotalamountbyyearone.Add(amountfisrt);

                    decimal amountsecond = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.employeeInfo.departmentId == dep.Id && x.salaryPeriod.salaryYear.yearName == (Convert.ToDateTime(DateTime.Now).Year-1).ToString()).Sum(x => x.amount);
                    deptotalamountbyyeartwo.Add(amountsecond);

                    decimal amountthird = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.employeeInfo.departmentId == dep.Id && x.salaryPeriod.salaryYear.yearName == (Convert.ToDateTime(DateTime.Now).Year - 2).ToString()).Sum(x => x.amount);
                    deptotalamountbyyearthree.Add(amountthird);


                }

                foreach (string dep in empdesignations)
                {
                    designationsname.Add(dep);

                    decimal amount = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.employeeInfo.designation == dep).Sum(x => x.amount);
                    designationwiseSalary.Add(amount);

                  


                }

                for (int i = 0; i <= 7; i++)
                {
                    years.Add(year);
                    decimal amount = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.salaryPeriod.salaryType.salaryTypeName == "Bonus" && x.salaryPeriod.salaryYear.yearName==year.ToString()).Sum(x => x.amount);
                    yearlybonus.Add(amount);
                    year = year + 1;

                }
                for (int i = 0; i <= monthno; i++)
                {
                    monthshow.Add(Convert.ToDateTime(premonth).ToString("MMMM"));
                    decimal amount = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.salaryPeriod.monthName == Convert.ToDateTime(premonth).ToString("MMMM") && x.salaryPeriod.salaryYear.yearName == Convert.ToDateTime(DateTime.Now).Year.ToString()).Sum(x => x.amount);
                    monthlySalary.Add(amount);
                    decimal taxamount= processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Income Tax" && x.salaryPeriod.monthName == Convert.ToDateTime(premonth).ToString("MMMM") && x.salaryPeriod.salaryYear.yearName == Convert.ToDateTime(DateTime.Now).Year.ToString()).Sum(x => x.amount);
                    taxlist.Add(taxamount);
                    decimal pfamount = processEmpSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Provident-Own" && x.salaryPeriod.monthName == Convert.ToDateTime(premonth).ToString("MMMM") && x.salaryPeriod.salaryYear.yearName == Convert.ToDateTime(DateTime.Now).Year.ToString()).Sum(x => x.amount);
                    pflist.Add(pfamount);
                    premonth = premonth.AddMonths(1);
                }



                models.Add(new PayrollDashboardcountViewModel
                {
                    months = monthshow,
                    totalmontlysalary = monthlySalary,
                    totaldepartmentwisesalary = departmentwiseSalary,
                    departmentname = departmentsname,
                    designationname=designationsname,
                    totaldesignationwisesalary=designationwiseSalary,
                    deptotalamountbyyearone=deptotalamountbyyearone,
                    deptotalamountbyyeartwo=deptotalamountbyyeartwo,
                    deptotalamountbyyearthree=deptotalamountbyyearthree,
                    averagebasic=averagebasic,
                    heighestbasic=heighestbasic,
                    averagetotal=averagetotal,
                    heighesttotal=heighesttotal,
                    pflist=pflist,
                    taxlist=taxlist,
                    totalprovisionsalarypercent=totalprovisionsalarypercent,
                    totalpermanentsalrypercent=totalpermanentsalrypercent,
                    years=years,
                    yearlybonus=yearlybonus




                });
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return models.FirstOrDefault();
        }
    }
}
