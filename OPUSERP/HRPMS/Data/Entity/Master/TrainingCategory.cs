using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("TrainingCategory", Schema = "HR")]
    public class TrainingCategory:Base
    {
        [Required]
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }

        public string trainingCategoryShortName { get; set; }
    }
}
