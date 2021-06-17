using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Data.Entity.MasterData
{
    public class SendEmailLogStatus:Base
    {
        public string sender { get; set; }
        public string receiver { get; set; }
        public string receiverEmail { get; set; }

        public DateTime? date { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string type { get; set; } //Send or Download

        public string module { get; set; }

        public string itemName { get; set; }
    }
}
