using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class FreedomFighterViewModel
    {
        public int? employeeID { get; set; }
        public int? FFID { get; set; }
        public string ffNo { get; set; }
        public string owner { get; set; }
        public string relationShip { get; set; }
        public string sectorNo { get; set; }
        public string remark { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public FreedomFighter freedomFighter { get; set; }
    }
}
