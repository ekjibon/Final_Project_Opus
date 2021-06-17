using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Event;

namespace OPUSERP.Areas.Event.Models
{
    public class EventData
    {
        public EventInformation eventInformation  { get; set; }
        public bool isRegistered { get; set; }
    }
}
