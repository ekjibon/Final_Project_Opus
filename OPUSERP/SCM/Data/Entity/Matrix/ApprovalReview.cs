using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("ApprovalReview", Schema = "SCM")]
    public class ApprovalReview:Base
    {
        public int? approvalLogID { get; set; }
        public ApprovalLog approvalLog { get; set; }

        public int userId { get; set; }
    }
}
