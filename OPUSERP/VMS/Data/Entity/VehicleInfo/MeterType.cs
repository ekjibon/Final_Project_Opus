using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("MeterType", Schema = "VMS")]
    public class MeterType:Base
    {
        [MaxLength(250)]
        public string meterTypeName { get; set; }
        public int? sortOrder { get; set; }
    }
}
