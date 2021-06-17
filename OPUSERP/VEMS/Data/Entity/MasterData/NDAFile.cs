using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.MasterData
{
    [Table("NDAFile", Schema = "VEMS")]
    public class NDAFile:Base
    {
        [Column(TypeName = "nvarchar(200)")]
        public string fileNmae { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string filePath { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string fileType { get; set; }

        public int? isActive { get; set; }
    }
}
