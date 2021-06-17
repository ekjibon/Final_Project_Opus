using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramSubject", Schema = "PM")]
    public class ProgramSubject:Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }

        public string subject { get; set; }
    }
}
