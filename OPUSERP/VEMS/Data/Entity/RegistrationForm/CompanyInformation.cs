using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("CompanyInformation", Schema = "VEMS")]
    public class CompanyInformation:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public string typeOfVendor { get; set; }

        public string businessNature { get; set; }

        public string officeTelephone { get; set; }

        public DateTime? dateOfEstablishment { get; set; }

        public int? permanentEmployee { get; set; }
        
        public decimal? investment { get; set; }
        
        public decimal? turnover { get; set; }
        
        public decimal? liquidityRatio { get; set; }

    }
}
