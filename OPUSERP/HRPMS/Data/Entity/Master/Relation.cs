using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("Relation", Schema = "HR")]
    public class Relation:Base
    {
        [Required]
        public string relationName { get; set; }
        public string relationNameBn { get; set; }

        public string relationShortName { get; set; }
    }
}
