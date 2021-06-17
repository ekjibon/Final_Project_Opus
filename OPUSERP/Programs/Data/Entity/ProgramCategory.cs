using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{
    [Table("ProgramCategory", Schema = "PM")]
    public class ProgramCategory:Base
    {
        public int? programMainCategoryId { get; set; }
        public ProgramMainCategory programMainCategory { get; set; }
        
        public string name { get; set; }
        
    }
}
