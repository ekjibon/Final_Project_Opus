using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramSourceFundApprove", Schema = "PM")]
    public class ProgramSourceFundApprove : Base
    {
        public int? programMasterId { get; set; }
        public ProgramMaster programMaster { get; set; }
        public string sourceName { get; set; }
        public decimal? percent { get; set; }
    }
}
