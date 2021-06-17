using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("Source", Schema = "CRM")]
    public class Source : Base
    {
        public string sourceName { get; set; }
    }
}
