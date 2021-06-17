using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("BillGenerateHistory", Schema = "CRM")]
    public class BillGenerateHistory : Base
    {
        public int? billGenerateId { get; set; }
        public BillGenerate billGenerate { get; set; }

        [MaxLength(250)]
        public string billNo { get; set; }
        [MaxLength(30)]
        public string invoiceNo { get; set; }
        public DateTime? billDate { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }


    }
}
