using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingInfoViewModel
    {
        public int Id { get; set; }
        public int? meetingId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string ccontent { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public int?[] checkbox { get; set; }
    }
}
