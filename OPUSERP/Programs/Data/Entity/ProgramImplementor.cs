using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramImplementor", Schema = "PM")]
    public class ProgramImplementor:Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }
        public string implementor { get; set; }
    }
}
