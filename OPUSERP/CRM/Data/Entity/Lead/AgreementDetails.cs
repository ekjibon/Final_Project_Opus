using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("AgreementDetails", Schema = "CRM")]
    public class AgreementDetails : Base
    {        
        public int? agreementId { get; set; }
        public Agreement agreement { get; set; }

        public int? ratingYearId { get; set; }
        public RatingYear ratingYear{ get; set; }

        public int? vatCategoryId { get; set; }
        public VatCategory vatCategory { get; set; }

        public int? taxCategoryId { get; set; }
        public TaxCategory taxCategory { get; set; }

        public DateTime? ratingDate { get; set; }

        
        public decimal? agreementAmount { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? ratingAmount { get; set; }
        public decimal? totalAmount { get; set; }
        [DefaultValue(0)]
        public int? isProposed { get; set; } //0=create,1=Proposed, 2=approve/verified, 3=review

        public string remarks { get; set; }
        public string revremarks { get; set; }
    }
}
