using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("SalesCollection", Schema = "OSales")]
    public class SalesCollection : Base
    {
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? collectionDate { get; set; }

        public decimal? collectionAmount { get; set; }

        public string collectionNumber { get; set; }

        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
        public string bankName { get; set; }
        public string checqueNo { get; set; }
        public string trxNo { get; set; }

        public decimal? Amount { get; set; }
        public int? storeId { get; set; }
        public Store store { get; set; }

        public int? salesInvoiceMasterId { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster { get; set; }


        //POS
        public decimal? cardAmount { get; set; }
        public decimal? mobileAmount { get; set; }

        public int? bankInfoId { get; set; }
        public Bank bankInfo { get; set; }

        public int? cardTypeId { get; set; }
        public CardType cardType { get; set; }

        public int? walletTypeId { get; set; }
        public WalletType walletType { get; set; }
    }
}
