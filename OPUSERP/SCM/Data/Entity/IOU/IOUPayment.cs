using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.IOU
{
    [Table("IOUPayment", Schema = "SCM")]
    public class IOUPayment:Base
    {
        public int? IOUId { get; set; }
        public IOUMaster IOU { get; set; }

        public int? iOUPaymentMasterId { get; set; }
        public IOUPaymentMaster iOUPaymentMaster { get; set; }
        

        [Column(TypeName = "decimal(18,2)")]
        public decimal? iouAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? paymentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? adjustmentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? balance { get; set; }

        public int? statusInfoId { get; set; }
        public StatusInfo statusInfo { get; set; }

        public int? paymentBy { get; set; }
        public DateTime? paymentDate { get; set; }

        public int? adjustmentBy { get; set; }
        public DateTime? adjustmentDate { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        public int? receivebyId { get; set; }
        public EmployeeInfo receiveby  { get; set; }

        
        public DateTime? receiveDate { get; set; }
        public string paymentMode { get; set; }
        public string chequeNo { get; set; }
        public string bankName { get; set; }
    }
}
