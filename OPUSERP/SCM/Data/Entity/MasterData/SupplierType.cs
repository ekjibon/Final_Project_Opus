using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("SupplierTypes", Schema = "SCM")]
    public class SupplierType:Base
    {
        [MaxLength(250)]
        public string supplierTypeName { get; set; }
    }
}
