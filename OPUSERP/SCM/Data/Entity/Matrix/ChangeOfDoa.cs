using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("ChangeOfDoa", Schema = "SCM")]
    public class ChangeOfDoa: Base
    {
        public int? userId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? isback { get; set; }
    }
}
