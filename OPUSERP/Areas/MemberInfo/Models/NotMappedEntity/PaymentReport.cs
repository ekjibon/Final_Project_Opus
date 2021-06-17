using OPUSERP.CLUB.Data.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSOPUSERP.Areas.MemberInfo.Models.NotMappedEntity
{
    public class PaymentReport
    {
        public int id { get; set; }
        public string name { get; set; }
        public string membershipId { get; set; }
        public string contactNumber { get; set; }

        public double openingDue { get; set; }

        public double openingPaid { get; set; }

        public IEnumerable<Collection> collections { get; set; }

        public IEnumerable<Invoice> invoices { get; set; }

        public double totalDue { get; set; }

        public double totalPaid { get; set; }

        public double totalReamin { get; set; }
    }
}
