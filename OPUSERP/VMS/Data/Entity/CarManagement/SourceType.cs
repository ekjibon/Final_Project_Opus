using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.CarManagement
{
    [Table("SourceType", Schema = "VMS")]
    public class SourceType:Base
    {
        public string sourceNameBN { get; set; }
        public string sourceName { get; set; }
    }
}
