using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Holiday", Schema = "HR")]
    public class Holiday : Base
    {
        public DateTime? weeklyHoliday { get; set; }
        public string holidayName { get; set; }
        public DateTime? holiday { get; set; }
        public string holidayNameBn { get; set; }
        public int? year { get; set; }
    }
}
