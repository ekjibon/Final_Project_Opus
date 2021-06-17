using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("RatingReview", Schema = "CRM")]
    public class RatingReview : Base
    {        
       

        public int? agreementDetailsId { get; set; }
        public AgreementDetails agreementDetails{ get; set; }

        public string remarks { get; set; }
        
        
    }
}
