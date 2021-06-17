using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.Supplier
{
    [Table("Contact", Schema = "SCM")]
    public class Contact:Base
    {
        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public string personName { get; set; }

        public string phoneNumber { get; set; }

        public string mobileNumber { get; set; }

        public int? resourceId { get; set; }
        public Resource resource { get; set; }
        
    }
}
