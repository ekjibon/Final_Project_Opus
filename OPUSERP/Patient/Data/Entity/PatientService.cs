using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Patient.Data.Entity
{
    [Table("PatientService", Schema = "HOSPTL")]
    public class PatientService : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? itemCategoryId { get; set; }
        public ItemCategory itemCategory { get; set; }

        public DateTime? serviceFromDate { get; set; }
        public DateTime? serviceToDate { get; set; }
        [MaxLength(20)]
        public string billingType { get; set; }
        public decimal? billAmount { get; set; }
        public int? iscomplete { get; set; }

   
       

        
    }
}
