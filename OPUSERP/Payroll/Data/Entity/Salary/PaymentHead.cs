using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("PaymentHead", Schema = "Club")]
    public class PaymentHead:Base
    {
        public string name { get; set; }
        public string remarks { get; set; }
        public string type { get; set; } // OneTime, MonthLy
    }
}
