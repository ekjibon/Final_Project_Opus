using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("RegistrationForm", Schema = "VEMS")]
    public class RegistrationForm:Base
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
        public CSItemCategory procurementCategory { get; set; }

        public string productServiceName { get; set; }

        public string partialDisagreement { get; set; }

        public string loginId { get; set; }

        public string password { get; set; }

        public int? approveType { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }
    }
}
