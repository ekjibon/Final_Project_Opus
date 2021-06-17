using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSEmployee.Models.Dashboard;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using OPUSERP.HRPMS.Services.Dashboard.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Dashboard
{
    public class HrmDashboardService:IHrmDashboardService
    {
        private readonly ERPDbContext _context;

        public HrmDashboardService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<ActiveEmployeeCountViewModel> GetActiveEmployeeCountViewModel()
        {
            int monthno = DateTime.Now.Month;
            DateTime premonth = DateTime.Now.AddMonths(monthno * -1);
            int year = DateTime.Now.Year-10;
            DateTime date = DateTime.Now.AddDays( - 7);
            List<int> years = new List<int>();
            for (int i=0; i < 10; i++)
            {
                years.Add(year);
                year=year + 1;

            }
            List<DateTime> days = new List<DateTime>();
            List<string> dayshow = new List<string>();
            List<DateTime> months = new List<DateTime>();
            List<string> monthshow = new List<string>();
            List<int> absentlist = new List<int>();
            List<int> latelist = new List<int>();
            List<int> absentlistmonth = new List<int>();
            List<int> leavelistmonth = new List<int>();
            List<int> leavelistcategory = new List<int>();
            List<string> leaveTypeslist = new List<string>();
            List<LeaveRegister> leaveRegisters = _context.leaveRegisters.Include(x=>x.leaveType).ToList();
            List<LeaveType> leaveTypes = _context.leaveTypes.ToList();
            List<ActiveEmployeeCountViewModel> activeEmployeeCountViewModel = new List<ActiveEmployeeCountViewModel>();
            var data = _context.employeeInfos.Include(x => x.department).Include(x => x.department).ToList();
            List<int?> departmentids = data.Select(x => x.departmentId).Distinct().ToList();
            List<int?> emploeetyoeids = data.Select(x => x.employeeTypeId).Distinct().ToList();
            List<Department> departments = _context.departments.Where(x => departmentids.Contains(x.Id)).ToList();
            List<EmployeeType> employeetypes = _context.employeeTypes.Where(x => emploeetyoeids.Contains(x.Id)).ToList();
            List<PromotionLog> promotionLogs = _context.promotionLogs.Where(x => x.date.Year == DateTime.Now.Year).ToList();
            List<ResignInformation> resignInformation=_context.resignInformation.Where(x => Convert.ToDateTime(x.resignDate).Year == DateTime.Now.Year).ToList();
            List<DepartmentWiseEmployeeCountViewModel> departmentWiseEmployeeCounts = new List<DepartmentWiseEmployeeCountViewModel>();
           List<ActiveEmployeeDetailView> activeEmployeeDetailViews = new List<ActiveEmployeeDetailView>();
            //List<EmpAttendance> empAttendances = _context.empAttendances.Where(x => Convert.ToDateTime(x.workDate).Date == DateTime.Now.Date).ToList();
            List<EmpAttendance> empAttendances = _context.empAttendances.Where(x => x.workDate == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).ToList();
            List<TraveInfo> traveInfos = _context.traveInfos.Include(x=>x.employee.department).Where(x => Convert.ToDateTime(x.startDate).ToString("yyyy") == Convert.ToDateTime(DateTime.Now).ToString("yyyy")).ToList();
            for (int i = 0; i <=7; i++)
            {
              
                dayshow.Add(Convert.ToDateTime(date).ToString("MMM dd"));
                absentlist.Add(empAttendances.Where(x => x.workDate == Convert.ToDateTime(date).ToString("yyyyMMdd") && x.summaryId==0).Count());
                latelist.Add(empAttendances.Where(x => x.workDate == Convert.ToDateTime(date).ToString("yyyyMMdd") && x.summaryId == 3).Count());
                date = date.AddDays(1);
            }
            for (int i = 0; i <= monthno; i++)
            {
                monthshow.Add(Convert.ToDateTime(premonth).ToString("MMMM"));

                absentlistmonth.Add(empAttendances.Where(x => x.workDate.Substring(0,6) == Convert.ToDateTime(premonth).ToString("yyyyMM") && x.summaryId == 0).Count());
                leavelistmonth.Add(leaveRegisters.Where(x => Convert.ToDateTime(x.leaveFrom).ToString("yyyyMM") == Convert.ToDateTime(premonth).ToString("yyyyMM")).Count());
                premonth = premonth.AddMonths(1);
            }
            foreach(LeaveType leavetype in leaveTypes)
            {
                leaveTypeslist.Add(leavetype.nameEn);
                leavelistcategory.Add(leaveRegisters.Where(x => Convert.ToDateTime(x.leaveFrom).ToString("yyyy") == Convert.ToDateTime(DateTime.Now).ToString("yyyy") && x.leaveTypeId == leavetype.Id).Count());
            }

            List<string> deparmentsname = new List<string>();
            List<string> employeeTypename = new List<string>();
            List<int> depmailcount = new List<int>();
            List<int> depfemailcount = new List<int>();
            List<int> yearwiseemployee = new List<int>();
            List<int> departmentswisepercent = new List<int>();
            List<int> employeetypepercent = new List<int>();
            List<int> travelcount = new List<int>();
            int employeepromotionrate = promotionLogs.Count() * 100 / data.Where(x => x.activityStatus == 1).Count();
            int turnover= resignInformation.Count() * 100 / data.Where(x => x.activityStatus == 1).Count();
            int absentRate= empAttendances.Where(x=>x.summaryId==0).Count()*100/ data.Where(x => x.activityStatus == 1).Count();
            foreach (Department depadrtment in departments)
            {
                int countM = data.Where(x => x.departmentId == depadrtment.Id && x.activityStatus == 1 && x.gender == "Male").Count();
                int countF = data.Where(x => x.departmentId == depadrtment.Id && x.activityStatus == 1 && x.gender == "Female").Count();
                int percentdep = data.Where(x => x.departmentId == depadrtment.Id && x.activityStatus == 1).Count()*100/data.Where(x =>x.activityStatus == 1).Count();
                int travelcounts = traveInfos.Where(x => x.employee.departmentId == depadrtment.Id).Count();
                deparmentsname.Add(depadrtment.deptName);
                depmailcount.Add(countM);
                depfemailcount.Add(countF);
                departmentswisepercent.Add(percentdep);
                travelcount.Add(travelcounts);



            }
            foreach (EmployeeType depadrtment in employeetypes)
            {
               
               
                int percentdep = data.Where(x => x.employeeTypeId == depadrtment.Id && x.activityStatus == 1).Count() * 100 / data.Where(x => x.activityStatus == 1).Count();
                employeeTypename.Add(depadrtment.empType);

                employeetypepercent.Add(percentdep);


            }
            foreach (int y in years)
            {
                int countemployee= data.Where(x => Convert.ToDateTime(x.joiningDatePresentWorkstation).Year == y).Count();
                yearwiseemployee.Add(countemployee);
            }

            activeEmployeeCountViewModel.Add(new ActiveEmployeeCountViewModel {
                totalActiveEmployee = data.Where(x => x.activityStatus == 1).Count(),
                totalMalePercent= data.Where(x => x.activityStatus == 1 && x.gender == "Male").Count()*100/data.Where(x => x.activityStatus == 1).Count(),
                totalFeMalePercent = data.Where(x => x.activityStatus == 1 && x.gender == "Female").Count() * 100 / data.Where(x => x.activityStatus == 1).Count(),
                activeEmployeeDetailViews =activeEmployeeDetailViews,
               departments=deparmentsname,
               depfemalecount=depfemailcount,
               depmalecount=depmailcount,
               years=years,
               noofemployeesbyyears=yearwiseemployee,
               departmentswisepercent=departmentswisepercent,
               employeTypes=employeeTypename,
               employeeTypePercent=employeetypepercent,
               employeepromotionrate= employeepromotionrate,
               turnOverrate= turnover,
               empAttendances=empAttendances,
                absentRate= absentRate,
                days=dayshow,
                months=monthshow,
                latelistseven=latelist,
                absentlistseven=absentlist,
                absentlistmonth=absentlistmonth,
                travelcount=travelcount,
                leavecatogies=leaveTypeslist,
                categoryleavecount= leavelistcategory,
                monthlyleavecount= leavelistmonth


            });
            return activeEmployeeCountViewModel.FirstOrDefault();
        }

    }
}
