//using JSON.Data.Entity.Inventory;
//using JSON.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReport4point3productionItemdetailViewModel
    {
       
        public string itemName { get; set; }
        public string hscode { get; set; }
        public decimal? totalused { get; set; }
        public decimal? price { get; set; }
        public decimal? depriciation { get; set; }
        public decimal? depriciationrate { get; set; }
        

    }
}
