using OPUSERP.Data.Entity;
using OPUSERP.Distribution.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("Area", Schema = "CRM")]
    public class Area : Base
    {       

        [Required]
        public string areaCode { get; set; }

        [Required]
        public string areaName { get; set; }

        public string areaLocation { get; set; }

        public int? areaId { get; set; }
        public Area area { get; set; }

        public int? salesLevelId { get; set; }
        public SalesLevel salesLevel { get; set; }
        [NotMapped]
        public string nameEnglish { get; set; }
    }
}
