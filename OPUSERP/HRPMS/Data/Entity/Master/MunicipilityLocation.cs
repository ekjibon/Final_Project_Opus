using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("MunicipilityLocation", Schema = "HR")]
    public class MunicipilityLocation:Base
    {
        [Required]
        public string locationName { get; set; }
        public string locationNameBn { get; set; }

        public string shortName { get; set; }
    }
}
