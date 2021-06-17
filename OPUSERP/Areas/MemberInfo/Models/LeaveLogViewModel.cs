using OPUSERP.Areas.MemberInfo.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class LeaveLogViewModel
    {
        public string employeeID { get; set; }

        public string leaveId { get; set; }

        public string whenLeave { get; set; }

        [Required]
        public int? type { get; set; }

        public string purpose { get; set; }

        [Required]
        public DateTime? from { get; set; }

        [Required]
        public DateTime? to { get; set; }

        public string employeeNameCode { get; set; }

        public LeaveLogLn fLang { get; set; }
    }
}
