using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("BankInformation", Schema = "VEMS")]
    public class BankInformation:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public string accountName { get; set; }

        public string accountNumber { get; set; }

        public string bankName { get; set; }

        public string branchName { get; set; }

        public string routingNumber { get; set; }
    }
}
