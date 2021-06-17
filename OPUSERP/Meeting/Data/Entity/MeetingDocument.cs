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
    [Table("MeetingDocument", Schema = "MMS")]
    public class MeetingDocument:Base
    {
        public string number { get; set; }

        public string subject { get; set; }

        public string content { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int? isClose { get; set; }

        public int? isInitial { get; set; }

        public int? isMeeting { get; set; }

        public string noteType { get; set; }

        public string summary { get; set; }

        public string decision { get; set; }

        public int? docId { get; set; }
        public MeetingDocument doc { get; set; }
    }
}
