using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.VEMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSHome.Models
{
    public class RegistrationViewModel
    {
        public string companyName { get; set; }

        public int? typeOfNDAAgree { get; set; }

        public string tradeLicenseNumber { get; set; }

        public string eTinNumber { get; set; }

        public string vatNumber { get; set; }

        public string contactPersonName { get; set; }

        public string contactNumber { get; set; }

        public string companyEmail { get; set; }

        public string alternativeEmail { get; set; }

        public int? procurementCategoryId { get; set; }

        public string productServiceName { get; set; }

        public string partialDisagreement { get; set; }

        public string loginId { get; set; }

        public string password { get; set; }

        public IEnumerable<RequiredDocuments> requiredDocuments { get; set; }
        public RequiredDocuments requiredDocument { get; set; }
        public IEnumerable<CodeOfContact> codeOfContacts { get; set; }
        public CodeOfContact codeOfContact { get; set; }
        public IEnumerable<NDAFile> nDAFiles { get; set; }
        public NDAFile nDAFile { get; set; }
        public IEnumerable<CSItemCategory> cSItemCategories { get; set; }
    }
}
