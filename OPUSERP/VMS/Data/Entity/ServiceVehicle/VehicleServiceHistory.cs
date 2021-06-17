using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("VehicleServiceHistory", Schema = "VMS")]
    public class VehicleServiceHistory:Base
    {
        public int vehicleInformationId { get; set; }
        public VehicleInformation vehicleInformation { get; set; }

        public int? supplierId { get; set; }
        public VMSSupplier supplier { get; set; }

        public DateTime? completionDate { get; set; }
        public DateTime? startDate { get; set; }
        [MaxLength(250)]
        public string referenceNo { get; set; }
        public string generalNotes { get; set; }


        public decimal? subTotal { get; set; }
        public decimal? laborTotal { get; set; }
        public decimal? partsTotal { get; set; }
        public string discountType { get; set; }
        public decimal? discount { get; set; }
        public string taxType { get; set; }
        public decimal? tax { get; set; }
        public decimal? total { get; set; }

        [MaxLength(250)]
        public string workorderNo { get; set; }
        [MaxLength(250)]
        public string billCommitteeNo { get; set; }
        public DateTime? workorderDate { get; set; }
        public DateTime? issueDate { get; set; }
        public DateTime? billCommitteeDate { get; set; }
        
    }
}
