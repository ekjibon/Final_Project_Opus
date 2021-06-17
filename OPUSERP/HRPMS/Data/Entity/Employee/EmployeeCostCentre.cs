using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeCostCentre", Schema = "HR")]
    public class EmployeeCostCentre : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? costCentreId { get; set; }
        public CostCentre costCentre { get; set; }

        public DateTime? costCentreDate { get; set; }



    }
}
