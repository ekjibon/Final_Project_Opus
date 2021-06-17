using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Patient.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.Patient.Models
{
    public class PatientAddressViewModel
    {
        public int patientAddressId { get; set; }
        public int? patientInfoId { get; set; }
        public int? addressTypeId { get; set; }
        public int? divisionId { get; set; }
        public int? districtId { get; set; }
        public int? thanaId { get; set; }
        public string maillingAddress { get; set; }
        public string postOfficeName { get; set; }
        public string postCode { get; set; }

        public Address company { get; set; }

        public IEnumerable<AddressType> addressTypes { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<Address> patientAddresses { get; set; }
    }
}
