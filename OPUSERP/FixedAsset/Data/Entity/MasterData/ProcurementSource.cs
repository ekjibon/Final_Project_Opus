using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.MasterData
{
    [Table("ProcurementSource", Schema = "FAMS")]
    public class ProcurementSource : Base
    {
        [MaxLength(150)]
        public string sourceName { get; set; }
    }
}
