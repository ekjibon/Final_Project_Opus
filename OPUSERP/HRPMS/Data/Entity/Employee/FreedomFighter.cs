using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("FreedomFighter", Schema = "HR")]
    public class FreedomFighter:Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }

        public string number { get; set; }

        public string owner { get; set; }

        public string relationship { get; set; }

        public string sectorNo { get; set; }

        public string remarks { get; set; }
    }
}
