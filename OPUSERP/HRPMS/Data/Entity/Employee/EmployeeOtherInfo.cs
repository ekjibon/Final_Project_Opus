using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeOtherInfo", Schema = "HR")]
    public class EmployeeOtherInfo : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? hRFacilityId { get; set; }
        public HRFacility hRFacility { get; set; }

        [MaxLength(200)]
        public string simsId { get; set; }
        [MaxLength(200)]
        public string busArea { get; set; }
        [MaxLength(200)]
        public string type { get; set; }



    }
}
