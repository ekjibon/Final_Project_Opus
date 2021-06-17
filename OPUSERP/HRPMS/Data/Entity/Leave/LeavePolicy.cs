using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Leave
{
    [Table("LeavePolicy", Schema = "HR")]
    public class LeavePolicy:Base
    {
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? leaveTypeId { get; set; }
        public LeaveType leaveType { get; set; }

        public int? yearId { get; set; }
        public Year year { get; set; }

        public int? yearlyMaxLeave { get; set; }
        public int? yearlyMaxCarry { get; set; }
        public string remarks { get; set; }
        public int? weeklyOffBridge { get; set; }
        public int? govtHolidayBridge { get; set; }
        [MaxLength(20)]
        public string paymentType { get; set; }
        public int? maxBridgeLimit { get; set; }
        public int? highestCarryForward { get; set; }
    }
}
