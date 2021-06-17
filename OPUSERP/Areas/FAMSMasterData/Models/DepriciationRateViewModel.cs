using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.FAMSMasterData.Models
{
    public class DepriciationRateViewModel
    {
        public int depriciationRateId { get; set; }       
        public string depreciationName { get; set; }
        public decimal? rate { get; set; }
        public int? usefulLife { get; set; }        
        public string formulaType { get; set; }

        public IEnumerable<DepriciationRate>  depriciationRates { get; set; }
        public IEnumerable<ItemCategory>  itemCategories { get; set; }
    }
}
