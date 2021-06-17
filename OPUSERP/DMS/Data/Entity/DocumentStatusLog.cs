using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("DocumentStatusLog", Schema = "Doc")]
    public class DocumentStatusLog : Base
    {
        public int? documentMasterId { get; set; }
        public DocumentMaster documentMaster { get; set; }

        [MaxLength(450)]
        public string documentStatus { get; set; }
    }
}
