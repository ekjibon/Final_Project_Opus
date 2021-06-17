using System;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetRegisterReportViewModel
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

        public string warrantyHead { get; set; }
        public string warrentyDate { get; set; }

        public string sourceName { get; set; }
        public string assetDescription { get; set; }
        public string assetSerialNo { get; set; }
        public string custodianName { get; set; }
        public string assetLocation { get; set; }
        public int? usefulLife { get; set; }
        public decimal? overhaulingCost { get; set; }
    }
}
