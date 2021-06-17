using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramIndicator", Schema = "PM")]
    public class ProgramIndicator:Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }

        public int? programOutPutId { get; set; }
        public ProgramOutPut programOutPut { get; set; }
        public string indicator { get; set; }
    }
}
