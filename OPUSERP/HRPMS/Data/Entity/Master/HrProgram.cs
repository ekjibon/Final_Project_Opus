using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("HrProgram", Schema = "HR")]
    public class HrProgram : Base
    {
        [MaxLength(250)]
        public string programName { get; set; }
        [MaxLength(250)]
        public string programNameBn { get; set; }
        [MaxLength(150)]
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
