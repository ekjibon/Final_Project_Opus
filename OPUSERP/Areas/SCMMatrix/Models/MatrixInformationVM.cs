namespace OPUSERP.Areas.SCMMatrix.Models
{
    public class MatrixInformationVM
    {
        public string projectName { get; set; }
        public int? projectId { get; set; }
        public int? userId { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public int? nextApproverId { get; set; }
        public string nextEmpName { get; set; }
        public string nextEmpCode { get; set; }
        public int? approverTypeId { get; set; }
        public string approverTypeName { get; set; }
        public int? matrixTypeId { get; set; }
        public string matrixTypeName { get; set; }
        public int? sequenceNo { get; set; }
        public int? specialBranchUnitId { get; set; }
        public int? isActive { get; set; }
    }
}
