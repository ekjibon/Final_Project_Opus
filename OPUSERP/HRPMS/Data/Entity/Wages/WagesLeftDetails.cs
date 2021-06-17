using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("WagesLeftDetails", Schema = "HR")]
    public class WagesLeftDetails : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public string reason { get; set; }

        public string remarks { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
        
    }
}
