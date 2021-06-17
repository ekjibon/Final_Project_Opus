using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Data.Entity.MasterData
{
    [Table("RegistrationType", Schema = "FAMS")]
    public class RegistrationType:Base
    {
        public string RegistrationTypeName { get; set; }
    }
}
