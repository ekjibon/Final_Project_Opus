using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("StoreDepartmentTypes", Schema = "SCM")]
    public class StoreDepartmentType:Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string deptName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string deptNameBn { get; set; }
        public int? shortOrder { get; set; }
    }
}

