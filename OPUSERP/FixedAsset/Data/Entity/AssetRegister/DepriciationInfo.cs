using OPUSERP.Data.Entity;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Data.Entity.AssetRegister
{
    [Table("DepriciationInfo", Schema = "FAMS")]
    public class DepriciationInfo : Base
    {
        public int? assetRegistrationId { get; set; }
        public AssetRegistration assetRegistration { get; set; }
        public int? depriciationPeriodId { get; set; }
        public DepriciationPeriod depriciationPeriod { get; set; }
    
        public decimal? depriciationRate { get; set; }

       

    }
}
