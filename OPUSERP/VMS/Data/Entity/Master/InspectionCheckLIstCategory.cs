using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("InspectionCheckLIstCategory", Schema = "VMS")]
    public class InspectionCheckLIstCategory:Base
    {
        public string checkListCategoryName { get; set; }
        public int order { get; set; }
    }
}
