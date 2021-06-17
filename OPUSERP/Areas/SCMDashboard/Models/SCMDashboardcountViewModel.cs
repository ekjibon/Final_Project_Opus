using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMDashboard.Models
{
    public class SCMDashboardcountViewModel
    {
        //1
        public List<string> departments{ get; set; }
        public List<int> deptotalrequisition { get; set; }
        public List<int> deptotalrequisitionpending { get; set; }
        public List<int> deptotalwoinprogress { get; set; }
        public List<int> deptotalwogenerated { get; set; }
        //2
        //public int deptotalrequisitionmonthlypercent { get; set; }
        //public int deptotalrequisitionpendingmonthlypercent { get; set; }
        //public int deptotalwoinprogressmonthlypercent { get; set; }
        //public int deptotalwogeneratedmonthlypercent { get; set; }
        public List<int> statuswisecount { get; set; }
        public List<string> statsinfo { get; set; }
        //3
        public List<decimal?> deptotalamount { get; set; }
        //4
        public List<decimal?> deptotalamountbyyear { get; set; }
        //5
        public List<int> depworkorderpercent { get; set; }
        //6
        public List<decimal?> depworkordervolume { get; set; }
        //7
        public List<int> topfiveitempercentwo { get; set; }
        public List<string> topfiveItem { get; set; }
        //8
        public List<int> yearlywocount { get; set; }
        public List<int> years { get; set; }
        //9,10
        public List<string> suppliers { get; set; }
        public List<decimal> supplierswisepercent { get; set; }
        public List<decimal> supplierswiseAmount { get; set; }
        //11,12
        public List<string> itemspurchase { get; set; }
        public List<decimal> itemwisepurchasepercent { get; set; }
        public List<decimal> itemwisepurchaseAmount { get; set; }
        //13
        public List<string> itemsrequisition { get; set; }
        public List<int> itemwiserequisitionpercent { get; set; }


    }
}
