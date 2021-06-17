using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("DocumentChart", Schema = "CRO")]
    public class DocumentChart : Base
    {
        [MaxLength(250)]
        public string documentName { get; set; }
        public string description { get; set; }
    }
}
