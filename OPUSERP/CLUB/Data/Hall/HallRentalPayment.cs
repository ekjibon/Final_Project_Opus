using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Hall
{
    public class HallRentalPayment : Base
    {
        public int? hallRentalApplicationInfoId { get; set; }
        public HallRentalApplicationInfo HallRentalApplicationInfo { get; set; }

        public int paymentMode {get;set;}
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string chequeNo { get; set; }
        public DateTime? paymentDate { get; set; }
        public decimal? amount { get; set; }

    }
}
