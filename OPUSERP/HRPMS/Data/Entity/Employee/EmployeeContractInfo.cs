using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("EmployeeContractInfo", Schema = "HR")]
    public class EmployeeContractInfo:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? contractStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? contractEndDate { get; set; }

        public string remarks { get; set; }
    }
}
