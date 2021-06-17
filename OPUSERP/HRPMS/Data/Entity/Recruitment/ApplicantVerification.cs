using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("ApplicantVerification", Schema = "HR")]
    public class ApplicantVerification : Base
    {
        public int MyProperty { get; set; }
    }
}
