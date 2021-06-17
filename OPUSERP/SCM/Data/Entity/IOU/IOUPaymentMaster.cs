using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.IOU
{
    [Table("IOUPaymentMaster", Schema = "SCM")]
    public class IOUPaymentMaster : Base
    {
        [Column(TypeName = "nvarchar(120)")]
        public string iouPaymentNo { get; set; }

        public DateTime? iouPaymentDate { get; set; }
        
        [Column(TypeName = "nvarchar(320)")]
        public string attentionTo { get; set; }

        public int? attentionToId { get; set; }

        public int iouPaymentStatus { get; set; }
        public string paymentBy { get; set; }
        public int? userId { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }

        public string remarks { get; set; }

        [NotMapped]
        public decimal? iouPaymentValue { get; set; }
        [NotMapped]
        public string pymentPerson { get; set; }
        [NotMapped]
        public string projectName { get; set; }
        [NotMapped]
        public string approverName { get; set; }
        [NotMapped]
        public string iouPaymentCurrentStatus { get; set; }



    }
}
