using Microsoft.AspNetCore.Mvc.Rendering;
using OPUSERP.CLUB.Data.Hall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Rental.Models
{
    public class HallRentalViewModel
    {
        //HallInfo
        public int Id { get; set; }
        public string hallName { get; set; }
        public string hallNameBn { get; set; }
        public string floor { get; set; }
        public string AgreementNumber { get; set; }
        public int seatCapacity { get; set; }

        public SelectList hallInfosss { get; set; }
        public HallInfo hallInfo { get; set; }
        public IEnumerable<HallInfo> hallInfos  { get; set; }
        public DateTime? submissionDate { get; set; }

        //HallRentalApplicationInfo
        public int hallInfoId { get; set; }
        public string chiefGuest { get; set; }
        public string specialGuest { get; set; }
        public string Subject { get; set; }
        public decimal? discount { get; set; }
        public int hallRentalShiftId { get; set; }
        public decimal? hallRent { get; set; }
        public DateTime? rentalDate { get; set; }
        public string rentalTime { get; set; }
        public string applicantName { get; set; }
        public string applicantOrganization { get; set; }
        public string applicantAddress { get; set; }
        public string mobileNo { get; set; }
        public string applicantSignUrl { get; set; }
        public int? status { get; set; } //applicaion=1,pending=2, approve=3, cancel=4
        public int? isPaid { get; set; }
        public string remarks { get; set; }
        public IEnumerable<HallRentalApplicationInfo>  hallRentalApplicationInfos  { get; set; }
        public HallRentalApplicationInfo hallRentalApplicationInfo  { get; set; }
        
        //HallRentalShift
        public string shiftName { get; set; }
        public string shiftNameBn { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public IEnumerable<HallRentalShift>  hallRentalShifts  { get; set; }
        public HallRentalShift  hallRentalShift  { get; set; }
        
        //hallRentalPayment
        public int hallRentalApplicationInfoId { get; set; }
        public int paymentMode { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string submissionNo { get; set; }
        public string chequeNo { get; set; }
        public DateTime? paymentDate { get; set; }
        public decimal? amount { get; set; }

    }
}
