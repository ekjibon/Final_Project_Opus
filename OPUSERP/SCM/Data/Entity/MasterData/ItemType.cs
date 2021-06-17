using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("ItemType", Schema = "SCM")]
    public class ItemType:Base
    {        
        [MaxLength(250)]
        public string itemTypeName { get; set; }       
    }
}
