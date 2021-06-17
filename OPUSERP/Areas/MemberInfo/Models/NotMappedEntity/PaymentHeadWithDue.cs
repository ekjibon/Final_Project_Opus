using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models.NotMappedEntity
{
    public class PaymentHeadWithDue
    {
        public PaymentHead paymentHead { get; set; }

        public decimal? Due { get; set; }
    }
}
