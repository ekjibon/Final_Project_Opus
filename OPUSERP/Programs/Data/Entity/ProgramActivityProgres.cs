using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramActivityProgres", Schema = "PM")]
    public class ProgramActivityProgres: Base
    {
        public int? programActivityId { get; set; }
        public ProgramActivity programActivity { get; set; }
        public DateTime? date { get; set; }

 
        public string description { get; set; }
    }
}
