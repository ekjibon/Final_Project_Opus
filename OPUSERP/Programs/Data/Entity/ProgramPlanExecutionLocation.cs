using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramPlanExecutionLocation", Schema = "PM")]
    public class ProgramPlanExecutionLocation:Base
    {
        public int? programActivityId { get; set; }
        public ProgramActivity programActivity { get; set; }
        public int? programYearId { get; set; }
        public ProgramYear programYear { get; set; }
        [MaxLength(10)]
        public string monthName { get; set; }
        public decimal? planQuantity { get; set; }

        public int? divisionId { get; set; }
        public Division division { get; set; }
        public int? districtId { get; set; }
        public District district { get; set; }
        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        public string maillingAddress { get; set; }

        
    }
}
