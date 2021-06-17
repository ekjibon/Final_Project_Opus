using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("HRFacility", Schema = "HR")]
    public class HRFacility : Base
    {
        [MaxLength(500)]
        public string facilityName { get; set; }
        [MaxLength(100)]
        public string facilityCode { get; set; }
    }
}
