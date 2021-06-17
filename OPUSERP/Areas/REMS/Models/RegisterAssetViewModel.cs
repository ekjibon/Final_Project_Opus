using System;

namespace OPUSERP.Areas.REMS.Models
{
    public class RegisterAssetViewModel
    {
        public int? id { get; set; }

        public string assetNo { get; set; }

        public string itemName { get; set; }

        public string poNo { get; set; }

        public string dept { get; set; }

        public string location { get; set; }

        public string supplierCode { get; set; }

        public string supplierName { get; set; }

        public DateTime? poDate { get; set; }

        public DateTime? warrentyStartOn { get; set; }

        public DateTime? warrentyFinishedAt { get; set; }

        public int? isWarrenty { get; set; }

        public string description { get; set; }
    }
}
