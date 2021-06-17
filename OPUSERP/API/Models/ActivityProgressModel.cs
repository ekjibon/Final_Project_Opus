using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class ActivityProgressModel
    {
        public int? progressReportId { get; set; }
        public int? progressActivityTypeId { get; set; }
        public string locationName { get; set; }
        public int? projectConstructionLocationId { get; set; }
        public string gridName { get; set; }
        public int? projectGridLocationId { get; set; }
        public string unit { get; set; }
        public decimal? totalQty { get; set; }
        public string forDayPlanned { get; set; }
        public string forDayAchived { get; set; }
        public string nextDayPlanned { get; set; }
        public string cumAchived { get; set; }
        public string compPercent { get; set; }

        public int? itemSpecificationId { get; set; }
        public string forDay { get; set; }
        public string forDayConsumption { get; set; }
        public string tillDate { get; set; }
        public string status { get; set; }//RECEIPTS,CONSUMPTION

        public string name { get; set; }
        public string planned { get; set; }
        public string actual { get; set; }
        public string description { get; set; }
        public string unitName { get; set; }
        public List<ActivityWiseDailyProgressDetail> activityWiseDailyProgressDetails { get; set; }
    }
}
