using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramMaster", Schema = "PM")]
    public class ProgramMaster : Base
    {
        public string number { get; set; }
        public string projectName { get; set; }
        public DateTime? date { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string place { get; set; }
        public string grantNumber { get; set; }
        public int? isDiscussion { get; set; }
        public string subject { get; set; }

        public decimal? totalBudget { get; set; }

        public int? programCategoryId { get; set; }
        public ProgramCategory programCategory { get; set; }

        public int? programYearId { get; set; }
        public ProgramYear programYear { get; set; }

        public int? thanaId { get; set; }
        public Thana thana { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

        public int? divisionId { get; set; }
        public Division division { get; set; }

        public int? programImpactId { get; set; }
        public ProgramImpact programImpact { get; set; }
    }
}
