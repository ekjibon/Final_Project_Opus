using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("VendorAddress", Schema = "VEMS")]
    public class VendorAddress:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public int? addressTypeId { get; set; }
        public AddressType addressType { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? divisionId { get; set; }
        public Division division { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

        public int? thanaId { get; set; }
        public Thana thana { get; set; }

        public string union { get; set; }

        public string postOffice { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }

        public string type { get; set; }  //Organization or Resourse
    }
}
