using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class OwnershipViewModel
    {
        public int ownershipTypeId { get; set; }
        public string OwnershipType { get; set; }

        public IEnumerable<OwnerShipType> ownerShipTypes { get; set; }
    }
}
