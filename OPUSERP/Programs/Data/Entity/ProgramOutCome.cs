using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramOutCome", Schema = "PM")]
    public class ProgramOutCome : Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }
        public int? programStatusId { get; set; }
        public ProgramStatus programStatus { get; set; }
        public string outcome { get; set; }
    }
}
