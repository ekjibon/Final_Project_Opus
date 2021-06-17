using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingInfo", Schema = "MMS")]
    public class MeetingInfo:Base
    {
        public string title { get; set; }

        public string type { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int? actionId { get; set; }
        public MeetingActionInfo action { get; set; }

        public int? isAchived { get; set; }

        public int? status { get; set; }//1=created;2=chairman Approved;3=meeting call.
    }
}
