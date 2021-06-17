using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class AuthPersonInformationViewModel
    {
        public int? companyId { get; set; }

        public int? authPersonInfoId { get; set; }

        public string name { get; set; }

        public string designation { get; set; }

        public string department { get; set; }

        public string contactNumber { get; set; }

        public string email { get; set; }

        public AuthorizedPerson authorizedPerson { get; set; }
    }
}
