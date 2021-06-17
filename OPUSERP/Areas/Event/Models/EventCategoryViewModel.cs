using OPUSERP.CLUB.Data.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Event.Models
{
    public class EventCategoryViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public IEnumerable<EventCategory> eventCategories { get; set; }
    }
}
