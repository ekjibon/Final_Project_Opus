using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.RegistrationForm
{
    [Table("VendorAttachment", Schema = "VEMS")]
    public class VendorAttachment:Base
    {
        public int? registrationFormId { get; set; }
        public RegistrationForm registrationForm { get; set; }

        public string fileFor { get; set; }

        public string fileNmae { get; set; }

        public string filePath { get; set; }

        public string fileType { get; set; }

        public string remark { get; set; }
    }
}
