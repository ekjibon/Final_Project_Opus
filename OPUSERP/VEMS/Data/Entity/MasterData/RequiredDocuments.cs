using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Data.Entity.MasterData
{
    [Table("RequiredDocuments", Schema = "VEMS")]
    public class RequiredDocuments:Base
    {
        public string description { get; set; }
    }
}
