using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;

namespace OPUSERP.DMS.Data.Entity
{
    [Table("DocumentDetail", Schema = "Doc")]
    public class DocumentDetail : Base
    {
        public int? documentMasterId { get; set; }
        public DocumentMaster documentMaster { get; set; }

        public int? fieldSettingId { get; set; }
        public FieldSettings fieldSetting { get; set; }

        public string fileldValue { get; set; }
    }
}
