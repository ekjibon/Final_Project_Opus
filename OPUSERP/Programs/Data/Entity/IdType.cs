using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Data.Entity
{

    [Table("IdType", Schema = "PM")]
    public class IdType : Base
    {
        public string typeName { get; set; }
    }
}
