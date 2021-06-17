using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("AtttachmentCategory", Schema = "HR")]
    public class AtttachmentCategory :  Base
    {
        public int? atttachmentGroupId { get; set; }
        public AtttachmentGroup atttachmentGroup { get; set; }

        [Required]
        public string categoryName { get; set; }
        [MaxLength(250)]
        public string categoryNameBn { get; set; }
    }
}
