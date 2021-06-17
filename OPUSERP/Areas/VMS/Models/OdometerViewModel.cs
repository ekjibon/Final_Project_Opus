using System;

namespace OPUSERP.Areas.VMS.Models
{
    public class OdometerViewModel
    {
        public int? vehicleInformationId { get; set; }
        
        public string odometerValue { get; set; }

        public DateTime? readingDate { get; set; }
    }
}
