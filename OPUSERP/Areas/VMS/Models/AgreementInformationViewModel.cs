using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Agreement;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class AgreementInformationViewModel
    {
        public int? agreementInfoId { get; set; }

        public int? vehicleId { get; set; }

        public int? supplierId { get; set; }

        public string agreementDate { get; set; }

        public string agreementBy { get; set; }

        public string expireDate { get; set; }

        
        public IEnumerable<AgreementCostHead> costHeads { get; set; }

        public IEnumerable<AgreementInformation> agreements { get; set; }

        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }

        public IEnumerable<LimitAmountType> limitAmountTypes { get; set; }

        public IEnumerable<LimitPeriodType> limitPeriodTypes { get; set; }

        public IEnumerable<VMSSupplier> suppliers { get; set; }
    }
}
