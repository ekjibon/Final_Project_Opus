using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Inventory
{
    [Table("PartsIssueDetails", Schema = "VMS")]
    public class PartsIssueDetails:Base
    {
        public int? partsIssueId { get; set; }
        public PartsIssueMaster partsIssue { get; set; }

        public int? partsDetailId { get; set; }
        public PurchasePartsDetail partsDetail { get; set; }
    }
}
