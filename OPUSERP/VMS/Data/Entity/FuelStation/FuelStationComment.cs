using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.FuelStation
{
    [Table("FuelStationComment", Schema = "VMS")]
    public class FuelStationComment: Base
    {
        public int? fuelStationInfoId { get; set; }
        public FuelStationInfo fuelStationInfo { get; set; }

        [MaxLength(250)]
        public string titles { get; set; }
        public string comments { get; set; }
    }
}
