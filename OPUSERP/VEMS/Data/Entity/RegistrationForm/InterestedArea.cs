using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("InterestedArea", Schema = "VEMS")]
    public class InterestedArea:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public string productName { get; set; }
    }
}
