using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("QualificationHead", Schema = "HR")]
    public class QualificationHead:Base
    {
        public string name { get; set; }
    }
}
