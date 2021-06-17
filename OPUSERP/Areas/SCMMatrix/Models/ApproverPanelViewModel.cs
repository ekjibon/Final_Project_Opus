using System;

namespace OPUSERP.Areas.SCMMatrix.Models
{
    public class ApproverPanelViewModel
    {
        public int? UserId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; } = string.Empty;
        public string EmailID { get; set; }
        public int? sequenceNo { get; set; }
        public string ProcessDate { get; set; }
        public string notes { get; set; }
        public int? nextApproverId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public int isActive { get; set; }
        public string imagePath { get; set; }
    }
}
