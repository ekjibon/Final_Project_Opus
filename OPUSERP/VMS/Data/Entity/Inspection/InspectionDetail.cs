using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.Master;

namespace OPUSERP.VMS.Data.Entity.Inspection
{
    [Table("InspectionDetail", Schema = "VMS")]
    public class InspectionDetail : Base
    {
        public int? inspectionMasterId { get; set; }
        public InspectionMaster inspectionMaster { get; set; }
        public int? inspectionCheckListCategoryId { get; set; }
        public InspectionCheckLIstCategory inspectionCheckListCategory { get; set; }
        public bool result { get; set; }
        public string filename{get;set;}
        public string comments { get; set; }
        public string filePath { get; set; }
    }
}
