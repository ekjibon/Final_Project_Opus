using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Distribution.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class AreaViewModel
    {
        public int areaId { get; set; }

        [Required]
        public string areaCode { get; set; }

        [Required]
        public string areaName { get; set; }

        public string areaLocation { get; set; }
        public int? masterareaid { get; set; }
        public int? salesLevelareaId { get; set; }

        public IEnumerable<Area> areas { get; set; }
        public IEnumerable<SalesLevel> salesLevels { get; set; }
    }
}
