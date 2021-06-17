using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.FuelStation
{
    [Table("FuelStationInfo", Schema = "VMS")]
    public class FuelStationInfo:Base
    {
        [MaxLength(150)]
        public string fuelStationName { get; set; }
        [MaxLength(150)]
        public string ownerName { get; set; }

        [MaxLength(150)]
        public string managerName { get; set; }

        [MaxLength(150)]
        public string tradelicenseNo { get; set; }



    }
}
