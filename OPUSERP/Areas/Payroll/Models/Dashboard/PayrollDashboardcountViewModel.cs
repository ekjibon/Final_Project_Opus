using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models.Dashboard
{
    public class PayrollDashboardcountViewModel
    {
        //1
        public List<string> months{ get; set; }
        public List<decimal> totalmontlysalary { get; set; }
     
        //2
        //public int deptotalrequisitionmonthlypercent { get; set; }
        //public int deptotalrequisitionpendingmonthlypercent { get; set; }
        //public int deptotalwoinprogressmonthlypercent { get; set; }
        //public int deptotalwogeneratedmonthlypercent { get; set; }
        public List<decimal> totaldepartmentwisesalary { get; set; }
        public List<string> departmentname { get; set; }
        //3
        public List<decimal> totaldesignationwisesalary { get; set; }
        public List<string> designationname { get; set; }
        //4
        public List<decimal> deptotalamountbyyearone { get; set; }
        public List<decimal> deptotalamountbyyeartwo { get; set; }
        public List<decimal> deptotalamountbyyearthree { get; set; }
        //5
        public decimal averagebasic { get; set; }
        public decimal heighestbasic { get; set; }
        //6
        public decimal averagetotal { get; set; }
        public decimal heighesttotal { get; set; }
        //7
        public List<decimal> taxlist { get; set; }

        //8
        public List<decimal> pflist { get; set; }
        //9
       
        public decimal totalprovisionsalarypercent { get; set; }
        public decimal totalpermanentsalrypercent { get; set; }
        //11
        public List<int> years { get; set; }
        public List<decimal> yearlybonus { get; set; }
       


    }
}
