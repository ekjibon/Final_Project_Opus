﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleBodySubTypeViewModel
    {
        public int? vehicleBodySubTypeId { get; set; }
        public string vehicleBodySubTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<VehicleBodySubType> vehicleBodySubTypes { get; set; }
    }
}
