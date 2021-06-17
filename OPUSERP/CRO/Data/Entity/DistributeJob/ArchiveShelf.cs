using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("ArchiveShelf", Schema = "CRO")]
    public class ArchiveShelf : Base
    {
        [MaxLength(350)]
        public string archiveShelfName { get; set; }
        [MaxLength(10)]
        public string initialCharactarOfFileName { get; set; }

        public int? archiveFloorId { get; set; }
        public ArchiveFloor archiveFloor { get; set; }

    }
}
