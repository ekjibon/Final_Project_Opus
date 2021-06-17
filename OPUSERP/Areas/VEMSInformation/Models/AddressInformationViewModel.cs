using OPUSERP.Data.Entity.Master;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class AddressInformationViewModel
    {
        public int? companyId { get; set; }

        public int? addressInfoId { get; set; }

        public int? addressTypeId { get; set; }

        public int? countryId { get; set; }

        public int? divisionId { get; set; }

        public int? districtId { get; set; }

        public int? thanaId { get; set; }

        public string union { get; set; }

        public string postOffice { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }

        public VendorAddress vendorAddress { get; set; }
        public IEnumerable<AddressType> addressTypes { get; set; }
        public IEnumerable<Country> countries { get; set; }

    }
}
