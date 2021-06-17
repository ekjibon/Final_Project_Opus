using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class CompanyInformationViewModel
    {
        public int? companyId { get; set; }

        public int? companyInfoId { get; set; }

        public string typeOfVendor { get; set; }

        public string businessNature { get; set; }

        public string officeTelephone { get; set; }

        public DateTime? dateOfEstablishment { get; set; }

        public int? permanentEmployee { get; set; }

        public decimal? investment { get; set; }

        public decimal? turnover { get; set; }

        public decimal? liquidityRatio { get; set; }

        public CompanyInformation companyInformation { get; set; }
    }
}
