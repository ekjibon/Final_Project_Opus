using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Result", Schema = "HR")]
    public class Result : Base
    {
        [Required]
        public string resultName { get; set; }
        public string resultNameBn { get; set; }
        
        public string resultShortName { get; set; }
        [Required]
        public decimal resultMaxValue { get; set; }
    }
}
