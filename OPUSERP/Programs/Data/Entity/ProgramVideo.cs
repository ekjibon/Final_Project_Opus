using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramVideo", Schema = "PM")]
    public class ProgramVideo:Base
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
    }
}
