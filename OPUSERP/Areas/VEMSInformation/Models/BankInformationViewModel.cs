using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSInformation.Models
{
    public class BankInformationViewModel
    {
        public int? companyId { get; set; }

        public int? bankInfoId { get; set; }

        public string accountName { get; set; }

        public string accountNumber { get; set; }

        public string bankName { get; set; }

        public string branchName { get; set; }

        public string routingNumber { get; set; }

        public BankInformation bankInformation { get; set; }
    }
}
