using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("JobSource", Schema = "RCRT")]
    public class JobSource : Base
    {
        public string jobSourceName { get; set; }
    }
}
