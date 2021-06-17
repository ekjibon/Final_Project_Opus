using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("Nominee", Schema = "HR")]
    public class Nominee:Base
    {
        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        public string address { get; set; }
        [MaxLength(100)]
        public string relation { get; set; }
        [MaxLength(40)]
        public string contact { get; set; }
        [MaxLength(20)]
        public string NID { get; set; }
        [MaxLength(150)]
        public string BRN { get; set; }
        public string imageUrl { get; set; }
        [MaxLength(250)]
        public string guardianName { get; set; }
        [MaxLength(250)]
        public string witnessName { get; set; }
        public DateTime? nomineeDate { get; set; }
    }
}
