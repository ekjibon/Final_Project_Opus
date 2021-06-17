using System;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.Requisition
{
    [Table("VehicleUse", Schema = "VMS")]
    public class VehicleUse : Base
    {
        public int? requisitionId { get; set; }
        public VMSRequisitionMaster requisition { get; set; }

        public int? vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public DateTime? journeyDate { get; set; }
        public string timetoleave { get; set; }
        public string journeyTime { get; set; }
        public string journeyFrom { get; set; }
        public string journeyDestination { get; set; }
        public string reason { get; set; }
        public decimal? odometerValuejourneyStart { get; set; }
        public decimal? odometerValuejourneyEnd { get; set; }
        public decimal? petrolOpeningStock { get; set; }
        public decimal? petrolSupply { get; set; }
        public decimal? petrolUse { get; set; }
        public string comments { get; set; }


    }
}
