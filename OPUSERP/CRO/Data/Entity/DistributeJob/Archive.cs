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
    [Table("Archive", Schema = "CRO")]
    public class Archive : Base
    {
        public int operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public DateTime? convertDate { get; set; }
        public DateTime? archiveDate { get; set; }
        public DateTime? reportPrintDate { get; set; }
        public DateTime? ratingDate { get; set; }
        public DateTime? ratingValidTillDate { get; set; }
        [MaxLength(150)]
        public string ratingValidDays { get; set; }
        [MaxLength(300)]
        public string archiveLocationSoftcopy { get; set; }

        public int? archiveShelfId { get; set; }
        public ArchiveShelf archiveShelf { get; set; }

        [MaxLength(300)]
        public string hardCopyNewFileNo { get; set; }
        [MaxLength(300)]
        public string hardCopyOldFileNo { get; set; }

        public int? hardWorkingFile { get; set; }
        public int? softWorkingFile { get; set; }
        public int? archiveFloorId { get; set; }
        

    }
}
