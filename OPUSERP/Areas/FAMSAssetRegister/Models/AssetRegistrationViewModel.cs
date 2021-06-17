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
    public class AssetRegistrationViewModel
    {
        public int? purchaseInfoId { get; set; }
        public int? supplierId { get; set; }
        public DateTime? purchaseDate { get; set; }
        public DateTime? receiveDate { get; set; }
        public DateTime? challanDate { get; set; }
        public string challanNo { get; set; }
        public decimal purchaseRate { get; set; }
        public decimal quantity { get; set; }
        public decimal? carringCost { get; set; }
        public decimal? additionalCost { get; set; }
        public string purchaseNo { get; set; }
        public int? registrationTypeId { get; set; }
        public int? purchaseId { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<RegistrationType> registrationTypes { get; set; }
        public IEnumerable<ItemSpecification> itemSpecifications { get; set; }


        public int? assetRegistrationId { get; set; }        
        public string assetNo { get; set; }
        public int? itemSpecificationId { get; set; }       
        public decimal? assetValue { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal assetquantity { get; set; }
        public decimal? assetcarringCost { get; set; }
        public decimal? assetadditionalCost { get; set; }
        public int? periodId { get; set; }
        public decimal? depriciationRate { get; set; }
        public DateTime Sdate { get; set; }
        public DateTime Edate { get; set; }
        public string[] headall { get; set; }
        public DateTime[] wdateall { get; set; }
        public string[] assetDescriptionall { get; set; }
        
        public string itemCatPre { get; set; }
        public string itemPrefix { get; set; }        
        public int? parentId { get; set; }
        public int? depriciationRateId { get; set; }

        public string[] assetCodeAll { get; set; }
        public string[] assetDescriptionAll { get; set; }        
        public string[] assetSerialNoAll { get; set; }
        public int? procurementSourceId { get; set; }

        public IFormFile imagePath_Challan { get; set; }
        public IFormFile imagePath_Warranty { get; set; }
        public IFormFile imagePath_Item_Font { get; set; }
        public IFormFile imagePath_Item_Back { get; set; }

        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public IEnumerable<DepriciationPeriod> depriciationPeriods { get; set; }
        public IEnumerable<DepriciationRate> depriciationRates { get; set; }
        public PurchaseInfo PurchaseInfo { get; set; }
        public IEnumerable<AssetWarrenty> assetWarrenties { get; set; }
        public DepriciationInfo DepriciationInfo { get; set; }
        public AssetRegistration AssetRegistration { get; set; }
        public IEnumerable<PurchaseOrderViewModel> purchaseOrderViewModels { get; set; }
        public PurchaseOrderViewModel PurchaseOrderViewModel { get; set; }
        public PurchaseOrderMaster purchaseOrderMaster { get; set; }
        public IEnumerable<StockViewModel> stockViewModels { get; set; }
        public IEnumerable<ProcurementSource> procurementSources { get; set; }
    }
}
