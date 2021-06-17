using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.FuelInfo
{
    [Table("FuelComment", Schema = "VMS")]
    public class FuelComment: Base
    {
        public int? fuelInformationId { get; set; }
        public FuelInformation fuelInformation { get; set; }

        [MaxLength(250)]
        public string title { get; set; }        
        public string comment { get; set; }
    }
}
