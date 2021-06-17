namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class AttendanceSummaryViewModel
    {
        public int Id { get; set; }
        public int employeeInfoId { get; set; }
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public string designation { get; set; }
        public int salaryPeriodId { get; set; }
        public string periodName { get; set; }
        public int? weeklyOff { get; set; }
        public int? leave { get; set; }
        public int? absent { get; set; }
        public int? holiday { get; set; }
        public int? present { get; set; }
        public int? late { get; set; }
        public int? lockLabel { get; set; }
        public string remarks { get; set; }
        //public string action { get; set; }
    }
}
