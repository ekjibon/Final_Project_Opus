using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Designation", Schema = "HR")]
    public class Designation:Base
    {
        [Required]
        public string designationCode { get; set; }

        [Required]
        public string designationName { get; set; }

        public string designationNameBN { get; set; }
  
        public string shortName { get; set; }

        //public int? empType { get; set; }
    }
}
