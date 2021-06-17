using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("AuthorizedPerson", Schema = "VEMS")]
    public class AuthorizedPerson:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public string name { get; set; }

        public string designation { get; set; }

        public string department { get; set; }

        public string contactNumber { get; set; }

        public string email { get; set; }
    }
}
