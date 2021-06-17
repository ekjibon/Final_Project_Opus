using OPUSERP.Areas.HRPMSLeave.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Leave;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LeaveTypeViewModel
{
    
    public int id { get; set; }

    [Required]
    public string leaveTypeNameEn { get; set; }
    public string leaveTypeNameBn { get; set; }
    public string description { get; set; }
    public LeaveLn fLang { get; set; }
    public IEnumerable<LeaveType> leaveTypes { get; set; }
}

