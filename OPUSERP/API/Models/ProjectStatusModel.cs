using Microsoft.AspNetCore.Http;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class ProjectStatusModel
    {
        public int? projectId { get; set; }
        public DateTime? reportDate { get; set; }
        public string reportNo { get; set; }
        public string remarks { get; set; }
        public string siteCondition { get; set; }
        public string siteWeather { get; set; }
        public int? statusId { get; set; }
        public string userName { get; set; }
        public string nextDayPlan { get; set; }
        public List<DailyProgressReport> dailyProgressReports { get; set; }
        public List<SiteConstraint> siteConstraints { get; set; }
        public List<SiteEquipment> siteEquipment { get; set; }
        //public List<SiteMaterial> siteMaterials { get; set; }
        public List<SiteVisitor> siteVisitors { get; set; }
        //public List<SiteManpower> siteManpowers { get; set; }
        public List<ActivityProgressModel> workProgressActivityDescriptions { get; set; }
        public List<DailyProgressReportModel> dailyProgressReportsVM { get; set; }
        public List<SiteConstraintModel> siteConstraintsVM { get; set; }
        public List<SiteEquipmentModel> siteEquipmentVM { get; set; }
        public List<SiteVisitorModel> siteVisitorsVM { get; set; }

    }
}
