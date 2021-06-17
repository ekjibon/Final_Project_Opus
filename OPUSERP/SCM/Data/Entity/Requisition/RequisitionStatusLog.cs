using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RequisitionStatusLogs", Schema = "SCM")]
    public class RequisitionStatusLog:Base
    {
        public int? requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public int? requisitionDetailId { get; set; }
        public RequisitionDetail requisitionDetail { get; set; }
        public int? userId { get; set; }
        public int? naUserId { get; set; }
        public int? nextApproverUserId { get; set; }
        public string Status { get; set; }
        public int? StatusID { get; set; }
        public int? CSMID { get; set; }
        public int? PPID { get; set; }
        public int? POID { get; set; }
        public string EntryDate { get; set; }
        public string Remarks { get; set; }
    }
}
