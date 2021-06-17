using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("Sector", Schema = "CRM")]
    public class Sector : Base
    {        
        public string sectorName { get; set; }

        public int? sectorId { get; set; }
        public Sector sector { get; set; }
    }
}
