using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosBillReturnPaymentDetail", Schema = "POS")]
    public class PosBillReturnPaymentDetail : Base
    {
        public int? billReturnPaymentMasterId { get; set; }
        public PosBillReturnPaymentMaster billReturnPaymentMaster { get; set; }

        public int? salesReturnInvoiceMasterId { get; set; }
        public PosSalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

    }
}
