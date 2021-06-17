using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramPeopleInDiscussion", Schema = "PM")]
    public class ProgramPeopleInDiscussion:Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }

        public string name { get; set; }

        public decimal? quantity { get; set; }
    }
}
