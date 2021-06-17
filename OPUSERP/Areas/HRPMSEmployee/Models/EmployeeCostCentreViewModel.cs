using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeCostCentreViewModel
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public int? costCentreId { get; set; }
        public DateTime? costCentreDate { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<CostCentre> costCentres { get; set; }
        public IEnumerable<EmployeeCostCentre> employeeCostCentres { get; set; }
    }
}
