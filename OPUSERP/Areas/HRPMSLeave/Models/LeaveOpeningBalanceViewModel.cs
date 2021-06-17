using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LeaveOpeningBalanceViewModel
{
    //[Required]
    public string employeeId { get; set; }
    [Required]
    public string employeeName { get; set; }
    [Required]
    public string designation { get; set; }
    [Required]
    public string department { get; set; }
    [Required]
    public int year { get; set; }
    [Required]
    public string leaveType { get; set; }
    [Required]
    public decimal openingBalance { get; set; }
    //[Required]
    public string carryDays { get; set; }
    
    public IEnumerable<Designation> designations { get; set; }
    public IEnumerable<Department> departments { get; set; }
    public IEnumerable<LeaveType> leaveTypes { get; set; }

}

