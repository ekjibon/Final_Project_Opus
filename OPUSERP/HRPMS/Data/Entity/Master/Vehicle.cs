using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Vehicle", Schema = "HR")]
    public class Vehicle:Base
    {
        [Required]
        public string vehicleType { get; set; }
        public string vehicleTypeBn { get; set; }

        public string vehicleShortName { get; set; }
    }
}
