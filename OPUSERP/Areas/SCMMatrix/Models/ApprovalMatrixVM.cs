namespace OPUSERP.Areas.SCMMatrix.Models
{
    public class ApprovalMatrixVM
    {
        public int?[] projectId { get; set; }

        public int?[] matrixTypeId { get; set; }

        public int?[] userId { get; set; }

        public int?[] nextApproverId { get; set; }

        public int?[] approverTypeId { get; set; }

        public int?[] isActive { get; set; }
        public int?[] Active { get; set; }

        public int[] sequenceNo { get; set; }
        public int? projectnid { get; set; }
        public int? matrixnid { get; set; }
    }
}
