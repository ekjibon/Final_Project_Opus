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
    public class AssetTransferReportViewModel
    {
        public Int64? rowSlNo { get; set; }
        public string purchaseDate { get; set; }        
        public string challanDate { get; set; }
        public string receiveDate { get; set; }
        public string assetNo { get; set; }
        public string challanNo { get; set; }
        public string itemName { get; set; }
        public string specificationName { get; set; }
        public string categoryName { get; set; }

        public decimal? unitPrice { get; set; }
        public decimal? totalAdditionalCost { get; set; }
        public decimal? rate { get; set; }
        public decimal? assetValue { get; set; }
        public decimal? accumuDepreciation { get; set; }
        public decimal? wDvalue { get; set; } 

        public string sourceName { get; set; }
        public string assetDescription { get; set; }
        public string assetSerialNo { get; set; }
        public string custodianName { get; set; }
        public string assetLocation { get; set; }
        public int? usefulLife { get; set; }

        public string transferLocation { get; set; }
        public string transferDate { get; set; }
    }
}
