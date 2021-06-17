using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Meeting.Models
{
    public class MeetingAddDocRouteViewModel
    {
        public int?[] ids { get; set; }
        public string noteType { get; set; }
        public int docId { get; set; }
        public int docRouteID { get; set; }
    }
}
