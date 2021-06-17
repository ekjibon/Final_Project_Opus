using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class MovementTrackingViewModel
    {

        public int Id { get; set; }
        public string empCode { get; set; }
        public string EmpName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? entryDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? LoginTime { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? LogoutTime { get; set; }


        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }
        public string Reason { get; set; }
        public int? Status { get; set; }
    }
}
