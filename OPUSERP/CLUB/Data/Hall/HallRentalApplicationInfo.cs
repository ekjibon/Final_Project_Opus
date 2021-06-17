using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Hall
{
    public class HallRentalApplicationInfo : Base
    {
        public int? hallInfoId { get; set; }
        public HallInfo hallInfo { get; set; }

        public int? hallRentalShiftId { get; set; }
        public HallRentalShift hallRentalShift { get; set; }

        public decimal? hallRent { get; set; }
        public DateTime? rentalDate { get; set; }
        public string rentalTime { get; set; }

        public string applicantName { get; set; }
        public string applicantOrganization { get; set; }
        public string applicantAddress { get; set; }
        public string mobileNo { get; set; }
        public string applicantSignUrl { get; set; }

        public DateTime? paymentDate { get; set; }
        public int? status { get; set; } //applicaion=1,pending=2, approve=3, cancel=4
        public int? isPaid { get; set; }
        public string chiefGuest{ get; set; }
        public string specialGuest { get; set; }
        public string remarks { get; set; }
    }
}
