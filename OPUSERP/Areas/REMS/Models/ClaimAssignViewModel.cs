using System;

namespace OPUSERP.Areas.REMS.Models
{
    public class ClaimAssignViewModel
    {
        public int? id { get; set; }

        public int? claimId { get; set; }

        public int? assetRegistrationId { get; set; }

        public string claimNumber { get; set; }

        public string claimUserName { get; set; }

        public string deliveryMode { get; set; }

        public int? claimUserId { get; set; }

        public DateTime? claimDate { get; set; }

        public DateTime? assignDate { get; set; }

        public string claimDescription { get; set; }

        public string problemDescription { get; set; }

        public string assignTypeName { get; set; }

        public string suppCode { get; set; }

        public int? supplierId { get; set; }

        public string supplierName { get; set; }

        public string assetName { get; set; }

        public string assetNo { get; set; }

        public int? userId { get; set; }

        public string empName { get; set; }

        public string remarks { get; set; }
        public int? statusId { get; set; }
        public string statusInfoName { get; set; }
    }
}
