using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramActivity", Schema = "PM")]
    public class ProgramActivity: Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }

        public int? ProgramOutPutId { get; set; }
        public ProgramOutPut ProgramOutPut { get; set; }        

        public string activity { get; set; }
        public string values { get; set; }
        public string description { get; set; }
        public string outComeName { get; set; }
        public string outPutName { get; set; }
        public string outPutValues { get; set; }
        public string indicator { get; set; }
        public string outputIndicator { get; set; }
    }
}
