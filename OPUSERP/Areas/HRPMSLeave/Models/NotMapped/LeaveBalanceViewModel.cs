namespace OPUSERP.Areas.HRPMSLeave.Models.NotMapped
{
    public class LeaveBalanceViewModel
    {
        public string leaveTypeName { get; set; }
        public int openingLeaveBalance { get; set; }
        public int yearlyMaxLeave { get; set; }
        public int leaveCarryDays { get; set; }
        public int leaveAvailed { get; set; }
        public int leaveBalance { get; set; }
        public int cumLeaveBalance { get; set; }
        public int userLeaveBalance { get; set; }
    }
}