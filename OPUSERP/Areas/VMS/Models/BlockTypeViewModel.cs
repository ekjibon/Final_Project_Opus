using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class BlockTypeViewModel
    {
        public int? blockTypeId { get; set; }
        public string blockTypeName { get; set; }
        public int? sortOrder { get; set; }

        public IEnumerable<BlockType> blockTypes { get; set; }
    }
}
