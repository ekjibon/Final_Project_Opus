using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("SiteMaterials", Schema = "SCM")]
    public class SiteMaterial:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public string forDay { get; set; }
        public string tillDate { get; set; }
        public string status { get; set; }//RECEIPTS,CONSUMPTION

    }
}
