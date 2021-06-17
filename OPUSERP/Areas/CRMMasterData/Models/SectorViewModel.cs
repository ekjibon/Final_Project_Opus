using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class SectorViewModel
    {
        public int? ssectorId { get; set; }
        public int? sssectorId { get; set; }
        public int? ParrentId { get; set; }
        public int? sId { get; set; }

        public string childSectorName { get; set; }      
        public string sectorName { get; set; }

        public IEnumerable<Sector> sectors { get; set; }
    }
}
