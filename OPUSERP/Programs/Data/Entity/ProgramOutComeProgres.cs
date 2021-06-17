using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramOutComeProgres", Schema = "PM")]
    public class ProgramOutComeProgres : Base
    {
        public int? programOutComeId { get; set; }
        public ProgramOutCome programOutCome { get; set; }

        public string outcome { get; set; }

        public int? programStatusId { get; set; }
        public ProgramStatus programStatus { get; set; }
    }
}
