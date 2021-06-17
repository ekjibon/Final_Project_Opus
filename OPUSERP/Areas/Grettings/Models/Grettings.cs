using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Grettings.Models
{
    public class Grettings
    {
        public string name { get; set; }
        public string memberName { get; set; }
        public string GrettingsMessage { get; set; }
        public DateTime? date { get; set; }
        public string email { get; set; }
        public string relation { get; set; }
    }
}
