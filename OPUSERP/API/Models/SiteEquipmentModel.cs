using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class SiteEquipmentModel
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
