using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramPlan", Schema = "PM")]
    public class programPlan:Base
    {
        public string number { get; set; }
        public DateTime? startDate { get; set; }
       
        public DateTime? endDate { get; set; }

        public int? programCategoryId { get; set; }
        public ProgramCategory programCategory { get; set; }

        public int? programYearId { get; set; }
        public ProgramYear programYear { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

        public int? divisionId { get; set; }
        public Division division { get; set; }
    }
}
