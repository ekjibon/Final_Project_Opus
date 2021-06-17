//using UMBRELLA.Areas.MasterData.Models.Lang;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.MasterData.Models
{
    public class AddressTypeViewModel
    {


        public int? addressTypeId { get; set; }
        public string typeName { get; set; }

        public IEnumerable<AddressType> addressTypes { get; set; }
      
    }
}
