using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class VehicleViewModel
    {
        public int vehicleId { get; set; }
        [Required]
        [Display(Name = "Vehicle Type")]
        public string vehicleType { get; set; }
        public string vehicleTypeBn { get; set; }

        public string vehicleShortName { get; set; }

        public VehicleInfoLn fLang { get; set; }
        public IEnumerable<Vehicle> vehicles { get; set; }
    }
}
