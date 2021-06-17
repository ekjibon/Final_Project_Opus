using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("Unit", Schema = "SCM")]
    public class Unit:Base
    {
        [MaxLength(250)]
        public string unitName { get; set; }
        [MaxLength(250)]
        public string description { get; set; }
    }
}
