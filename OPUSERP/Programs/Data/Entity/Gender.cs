using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{

    [Table("Gender", Schema = "PM")]
    public class Gender:Base
    {
        public string name { get; set; }
    }
}
