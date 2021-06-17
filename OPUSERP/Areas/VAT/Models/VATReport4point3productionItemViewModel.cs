//using JSON.Data.Entity.Inventory;
//using JSON.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReport4point3productionItemViewModel
    {
        public string HSCode { get; set; }
        public string itemName { get; set; }
        public string unitName { get; set; }
        public DateTime? bomDate { get; set; }
        public List<VATReport4point3productionItemdetailViewModel> vATReport4Point3ProductionItemdetailViewModels { get; set; }
        public List<VATReport4point3productionledgerdetailViewModel> vATReport4Point3ProductionledgerdetailViewModels { get; set; }
        

    }
}
