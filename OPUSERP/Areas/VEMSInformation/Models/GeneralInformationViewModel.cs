using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class GeneralInformationViewModel
    {
        public int? companyId { get; set; }

        public string companyName { get; set; }

        public string tradeLicenseNumber { get; set; }

        public string eTinNumber { get; set; }

        public string vatNumber { get; set; }

        public string contactPersonName { get; set; }

        public string contactNumber { get; set; }

        public string companyEmail { get; set; }

        public string alternativeEmail { get; set; }

        public RegistrationForm registrationForm { get; set; }
        public CompanyInformation companyInformation { get; set; }
        public IEnumerable<BankInformation> bankInformation { get; set; }
        public IEnumerable<AuthorizedPerson> authorizedPerson { get; set; }
        public IEnumerable<TopOfficial> topOfficials { get; set; }
        public IEnumerable<VendorAddress> vendorAddresses { get; set; }
        public IEnumerable<VendorAttachment> vendorAttachments { get; set; }
        public IEnumerable<InterestedArea> interestedAreas { get; set; }
    }
}
