using Microsoft.AspNetCore.Http;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetRegisterSummaryReportViewModel
    {
        public int? categoryId { get; set; }       
        public string categoryName { get; set; }

        public decimal? openingBalance { get; set; }
        public decimal? addition { get; set; }
        public decimal? balance { get; set; }
        public decimal? oBDepreciation { get; set; }
        public decimal? accumuDepreciation { get; set; }
        public decimal? wDvalue { get; set; }      
        
    }
}
