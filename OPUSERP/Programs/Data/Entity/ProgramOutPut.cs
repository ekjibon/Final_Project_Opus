using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramOutPut", Schema = "PM")]
    public class ProgramOutPut : Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }

        public int? programOutComeId { get; set; }
        public ProgramOutCome programOutCome { get; set; }

        public int? programStatusId { get; set; }
        public ProgramStatus programStatus { get; set; }
        public string output { get; set; }
    }
}
