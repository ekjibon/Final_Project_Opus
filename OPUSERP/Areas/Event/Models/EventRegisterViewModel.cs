using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Event.Models
{
    public class EventRegisterViewModel
    {
        public int? eventId { get; set; }

        public int?[] heads { get; set; }

        public int?[] qnt { get; set; }

        public string transectionId { get; set; }

        public int? employeeId { get; set; }

        public decimal? invoiceAmount { get; set; }
    }
}
