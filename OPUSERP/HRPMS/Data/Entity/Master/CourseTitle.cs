using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("CourseTitle", Schema = "HR")]
    public class CourseTitle : Base
    {
        public string nameEN { get; set; }

        public string nameBN { get; set; }

        public string remarks { get; set; }
    }
}
