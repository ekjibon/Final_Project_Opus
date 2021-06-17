using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Bulk
{
    [Table("BulkHistory", Schema = "Club")]
    public class BulkHistory : Base
    {
        public string number { get; set; }
        public string text { get; set; }
        public int? employeeId { get; set; }
        public int? groupId { get; set; }
        public int type { get; set; } // From Group = 1 Or Individuals = 2

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
    }
}
