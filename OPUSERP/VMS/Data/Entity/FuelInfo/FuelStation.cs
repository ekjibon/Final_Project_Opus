using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;

namespace OPUSERP.VMS.Data.Entity.FuelInfo
{
    [Table("FuelStation", Schema = "VMS")]
    public class FuelStation: Base
    {
        [MaxLength(350)]
        public string fuelStationName { get; set; }
        [MaxLength(20)]
        public string phoneNumber { get; set; }
        [MaxLength(100)]
        public string webSite { get; set; }
    }
}
