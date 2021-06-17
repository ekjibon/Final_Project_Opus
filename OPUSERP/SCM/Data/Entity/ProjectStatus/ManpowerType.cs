using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("ManpowerTypes", Schema = "SCM")]
    public class ManpowerType:Base
    {
        public string manpowerTypeName { get; set; }
        public int? shortOrder { get; set; }
    }
}
