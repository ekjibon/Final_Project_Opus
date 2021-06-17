using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosCollectionMaster", Schema = "POS")]
    public class PosCollectionMaster : Base
    {
        public int? posCustomerId { get; set; }
        public PosCustomer posCustomer { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? collectionDate { get; set; }

        public decimal? collectionAmount { get; set; }
        [MaxLength(100)]
        public string collectionNumber { get; set; }

        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }

        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
        public decimal? cardAmount { get; set; }

        public decimal? mobileAmount { get; set; }

        public int? bankInfoId { get; set; }
        public BankInfo bankInfo { get; set; }

        public int? cardTypeId { get; set; }
        public CardType cardType { get; set; }

        public int? mobileBankingTypeId { get; set; }
        public MobileBankingType mobileBankingType { get; set; }
        [MaxLength(200)]
        public string trxNumber { get; set; }

        public string remarks { get; set; }
        public int? storeId { get; set; }
        public Store store { get; set; }
    }
}
