namespace OPUSERP.Areas.SCMPurchaseProcess.Models
{
    public class RequisitionForCSViewModel
    {
        public int? Id { get; set; }
        public int? projectId { get; set; }
        public string reqNo { get; set; }
        public string reqDate { get; set; }
        public string targetDate { get; set; }
        public string justification { get; set; }
        public string reqDept { get; set; }
        public int? statusInfoId { get; set; }
        public string csStatus { get; set; }
        public int? csStatusID { get; set; }
        public string csNo { get; set; }
        public string jobAssignDate { get; set; }
        public string subject { get; set; }
        public string projectName { get; set; }
        public int? isCS { get; set; }
    }
}
