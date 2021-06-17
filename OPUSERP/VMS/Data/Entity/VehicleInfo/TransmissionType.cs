using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.VehicleInfo
{
    [Table("TransmissionType", Schema = "VMS")]
    public class TransmissionType:Base
    {
        [MaxLength(250)]
        public string transmissionTypeName { get; set; }
        public int? sortOrder { get; set; }
    }
}
