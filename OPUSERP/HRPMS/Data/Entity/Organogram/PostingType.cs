using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("PostingType", Schema = "HR")]
    public class PostingType: Base
    {
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }
    }
}
