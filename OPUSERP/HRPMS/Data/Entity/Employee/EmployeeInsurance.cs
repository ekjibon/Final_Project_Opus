using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeInsurance", Schema = "HR")]
    public class EmployeeInsurance:Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        [MaxLength(350)]
        public string name { get; set; }       
        [MaxLength(100)]
        public string relation { get; set; }
        [MaxLength(20)]
        public string NID { get; set; }
        [MaxLength(100)]
        public string age { get; set; }
        [MaxLength(10)]
        public string gender { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public DateTime? insuranceDate { get; set; }
        public string imageUrl { get; set; }

        
    }
}
