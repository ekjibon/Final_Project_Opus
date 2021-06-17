using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramYear", Schema = "PM")]
    public class ProgramYear:Base
    {
        public string name { get; set; }
    }
}
