using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Agreement
{
    [Table("AgreementInformation", Schema = "VMS")]
    public class AgreementInformation:Base
    {
        public int? vehicleId { get; set; }
        public VehicleInformation vehicle { get; set; }

        public int? supplierId { get; set; }
        public VMSSupplier supplier { get; set; }

        public string agreementDate { get; set; }

        public string agreementBy { get; set; }

        public string expireDate { get; set; }
    }
}
