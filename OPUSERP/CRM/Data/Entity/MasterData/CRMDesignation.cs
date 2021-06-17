using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("CRMDesignation", Schema = "CRM")]
    public class CRMDesignation : Base
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
