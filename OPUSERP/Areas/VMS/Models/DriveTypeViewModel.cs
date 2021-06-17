using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class DriveTypeViewModel
    {
        public int? driveTypeId { get; set; }
        public string driveTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<DriveType> driveTypes { get; set; }
    }
}
