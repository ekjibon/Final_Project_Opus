using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Data.Entity.ProjectStatus
{
    [Table("SiteEquipments", Schema = "SCM")]
    public class SiteEquipment:Base
    {
        public int? progressReportId { get; set; }
        public DailyProgressReport progressReport { get; set; }
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }
        public string categoryName { get; set; }
        public string equipmentNo { get; set; }
        public string equipmentHours { get; set; }
        public string status { get; set; }
    }
}
