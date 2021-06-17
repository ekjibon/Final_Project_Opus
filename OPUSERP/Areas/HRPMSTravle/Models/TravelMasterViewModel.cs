using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTravle.Models
{
    public class TravelMasterViewModel
    {

        public string userName { get; set; }

        public int? travelID { get; set; }

        public int? id { get; set; }

        public string travelNumber { get; set; }

        public int? employeeID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public string accountNumber { get; set; }

        public int? travelProjectId { get; set; }

        public int? travelActivityId { get; set; }

        public int? travelDonerId { get; set; }

        public string purpose { get; set; }

        public string travelApprove { get; set; }

        // TravellerInfo
        public int?[] travellerEmployeeList { get; set; }

        //TravellingInfo
        public string[] travellingFrom { get; set; }

        public string[] travellingTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime?[] startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime?[] arrivalDate { get; set; }

        public string[] startTime { get; set; }

        public string[] arrivalTime { get; set; }

        public int?[] travelVehicleTypeId { get; set; }

        public string[] vehicleNumber { get; set; }

        public string[] driverName { get; set; }

        public string[] driverContactNumber { get; set; }

        public string[] accommodationDaaress { get; set; }

        public int?[] bookingRequird { get; set; }

        public IEnumerable<HRDoner> travelDoners { get; set; }
        public IEnumerable<HRActivity> travelActivities { get; set; }
        public IEnumerable<TravelVehicleType> travelVehicleTypes { get; set; }
        public IEnumerable<HRProject> hRProjects { get; set; }
        public IEnumerable<TravelRoute> travelRoutes { get; set; }
        public IEnumerable<TravelMaster> travelMasters { get; set; }
        public IEnumerable<TravelStatusLog> travelStatusLogs { get; set; }

    }
}
