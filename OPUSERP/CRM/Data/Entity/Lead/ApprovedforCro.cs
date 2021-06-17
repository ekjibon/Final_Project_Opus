using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("ApprovedforCro", Schema = "CRM")]
    public class ApprovedforCro : Base
    {      

        public int? agreementDetailsId { get; set; }
        public AgreementDetails agreementDetails { get; set; }

        public DateTime? approvedDate { get; set; }
        [DefaultValue(0)]
        public int? isRated { get; set; } //0=Normal, 1=previous
    }
}
