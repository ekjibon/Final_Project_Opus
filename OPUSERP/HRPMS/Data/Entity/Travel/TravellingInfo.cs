using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Travel
{
    [Table("TravellingInfo", Schema = "HR")]
    public class TravellingInfo:Base
    {
        public int? travelMasterId { get; set; }
        public TravelMaster travelMaster { get; set; }

        public string travellingFrom { get; set; }

        public string travellingTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? arrivalDate { get; set; }

        public string startTime { get; set; }

        public string arrivalTime { get; set; }

        public int? travelVehicleTypeId { get; set; }
        public TravelVehicleType travelVehicleType { get; set; }

        public string vehicleNumber { get; set; }

        public string driverName { get; set; }

        public string driverContactNumber { get; set; }

        public string accommodationDaaress { get; set; }

        public int? bookingRequird { get; set; } //1=Yes,0=No
    }
}
