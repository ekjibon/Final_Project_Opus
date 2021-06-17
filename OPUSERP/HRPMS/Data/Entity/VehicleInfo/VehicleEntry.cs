using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.VehicleInfo
{
    [Table("VehicleEntry", Schema = "HR")]
    public class VehicleEntry : Base
    {
        public int? vehicleId { get; set; }
        public Vehicle vehicle { get; set; }

        public string workStation { get; set; }

        [MaxLength(250)]
        public string registrationNo { get; set; }

        [MaxLength(100)]
        public string engineNo { get; set; }

        public string bodyType { get; set; }

        public string manufacturingYear { get; set; }

        public string horsePower { get; set; }

        public string batteryType { get; set; }

        [Required]
        public string vID { get; set; }

        public string department { get; set; }

        public string chassisNo { get; set; }

        public string noofSeat { get; set; }

        public string noofCylinder { get; set; }

        public string distributorType { get; set; }

        public string oilType { get; set; }

        public string description { get; set; }

        public string source { get; set; }//Govt Budget or Project Budget

        public string purchesAmount { get; set; }

        public string insurance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfPurches { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfHandoverToMOPA { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? registrationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? renewalDate { get; set; }

        public string renewalOffice { get; set; }

        [ForeignKey("EmployeeInfo")]
        public int? officerId { get; set; }
        public string officerName { get; set; }

        [ForeignKey("EmployeeInfo")]
        public int? driverId { get; set; }
        public string driverName { get; set; }

        //For Type Managing 
        [MaxLength(100)]
        public string orgType { get; set; }
    }
}
