using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("ReportField", Schema = "VMS")]
    public class ReportField:Base
    {
        public string fieldValue { get; set; }

        public string originName { get; set; }
    }
}
