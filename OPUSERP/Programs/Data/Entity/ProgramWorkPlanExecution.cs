using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramWorkPlanExecution", Schema = "PM")]
    public class ProgramWorkPlanExecution : Base
    {
        public int? programActivityId { get; set; }
        public ProgramActivity programActivity { get; set; }

        public int? programYearId { get; set; }
        public ProgramYear programYear { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? firstMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? secondMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? thirdMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? fourthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? fifthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? sixthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? seventhMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? eighthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ninethMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? tenthMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? eleventhMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? twelvethMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? subTotal { get; set; }

        public string remarks { get; set; }
    }
}
