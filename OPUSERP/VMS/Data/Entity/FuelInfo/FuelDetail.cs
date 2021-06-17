using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.FuelInfo
{
    [Table("FuelDetail", Schema = "VMS")]
    public class FuelDetail : Base
    {
        public int? fuelInformationId { get; set; }
        public FuelInformation fuelInformation { get; set; }

        public int? fuelTypeId { get; set; }
        public FuelType fuelType { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
    }
}
