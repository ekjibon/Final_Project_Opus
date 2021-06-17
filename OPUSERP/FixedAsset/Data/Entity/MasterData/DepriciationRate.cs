using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.FixedAsset.Data.Entity.MasterData
{
    [Table("DepriciationRate", Schema = "FAMS")]
    public class DepriciationRate : Base
    {
        [MaxLength(400)]
        public string depreciationName { get; set; }
        public decimal? rate { get; set; }
        public int? usefulLife { get; set; }
        [MaxLength(20)]
        public string formulaType { get; set; }

    }
}
