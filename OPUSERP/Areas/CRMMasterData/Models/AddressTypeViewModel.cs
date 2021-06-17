using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class AddressTypeViewModel
    {
        public int addressTypeId { get; set; }

        [Required]
        public string typeName { get; set; }

        public IEnumerable<AddressType> addressTypes { get; set; }
    }
}
