using OPUSERP.CLUB.Data.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models.NotMappedEntity
{
    public class SingleFees
    {
        public int id { get; set; }
        public string name { get; set; }
        public string membershipId { get; set; }
        public string contactNumber { get; set; }
        public double haveToPay { get; set; }
        public double paid { get; set; }
        public IEnumerable<PaymentLog> paymentLogs { get; set; }
    }
}
