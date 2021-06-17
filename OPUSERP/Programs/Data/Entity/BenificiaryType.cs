using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{

    [Table("DateRange", Schema = "PM")]
    public class DateRange:Base
    {
        public string name { get; set; }
    }
}
