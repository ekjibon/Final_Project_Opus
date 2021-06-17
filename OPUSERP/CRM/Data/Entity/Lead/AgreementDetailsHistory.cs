using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("AgreementDetailsHistory", Schema = "CRM")]
    public class AgreementDetailsHistory : Base
    {
        public int? agreementId { get; set; }
        public Agreement agreement { get; set; }

        public int? agreementDetailsId { get; set; }

        public decimal? vatAmount { get; set; }

        public decimal? ratingAmount { get; set; }

        public decimal? totalAmount { get; set; }


    }
}
