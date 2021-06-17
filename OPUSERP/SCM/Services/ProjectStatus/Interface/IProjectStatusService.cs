using OPUSERP.API.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.ProjectStatus.Interface
{
    public interface IProjectStatusService
    {
        Task<int> SaveDailyProgressReport(DailyProgressReport dailyProgress);
        Task<DailyProgressReport> GetDailyProgressReportsById(int id);
        Task<IEnumerable<DailyProgressReport>> GetDailyProgressReportByFiltering(DateTime? fromDate, DateTime? toDate, int projectId);
        Task<int> GetTotalReport(int projectId);
        Task<IEnumerable<DailyProgressReport>> GetDailyProgressReportList(string userName);
        Task<DailyProgressReport> GetDailyProgressReportByDay(string date, string userName);
        Task<int> SaveWorkProgressActivityDescriptionSingle(WorkProgressActivityDescription workProgressActivity);
        Task<IEnumerable<WorkProgressActivityDescription>> GetWorkProgressActivityTypeByReportId(int reportId);
        Task<bool> SaveWorkProgressActivityDescription(List<WorkProgressActivityDescription> workProgress);
        Task<IEnumerable<WorkProgressActivityType>> GetWorkProgressActivityType();
        Task<bool> SaveWorkProgressActivityType(WorkProgressActivityType workProgressActivityType);
        Task<bool> SaveActivityProgressItemDetails(List<ActivityWiseDailyProgressDetail> workProgress);
        Task<IEnumerable<ActivityWiseDailyProgressDetail>> GetActivityProgressItemDetailsByActivityId(int activityId);
        Task<IEnumerable<ActivityWiseDailyProgressDetail>> GetActivityProgressItemDetailsByReportId(int reportId);
        Task<bool> SaveSiteEquipment(List<SiteEquipment> siteEquipment);
        Task<bool> SaveSiteMaterial(List<SiteMaterial> siteMaterial);
        Task<bool> SaveSiteManpower(List<SiteManpower> siteManpower);
        Task<IEnumerable<SiteManpower>> GetSiteManpowerByProgressId(int progressId);
        Task<bool> SaveSiteVisitor(List<SiteVisitor> siteVisitor);
        Task<IEnumerable<SiteVisitor>> GetSiteVisitorByProgressId(int progressId);
        void UpdateVisitorById(int id, string imagePath);
        Task<bool> SaveSiteConstraints(List<SiteConstraint> siteConstraint);
        Task<IEnumerable<SiteConstraint>> GetSiteConstraintByProgressId(int progressId);
        Task<IEnumerable<SiteEquipment>> GetSiteEquipmentByReportId(int reportId);
        Task<int> SaveProjectLocation(ProjectConstructionLocation projectConstructionLocation);
        Task<IEnumerable<ProjectConstructionLocation>> GetAllProjectLocation();
        Task<IEnumerable<ProjectConstructionLocation>> GetAllProjectLocationByProjectId(int projectId,string userName);
        Task<ProjectConstructionLocation> GetProjectLocationById(int id);

        #region Grid Location
        Task<int> SaveProjectGridLocation(ProjectGridLocation projectGridLocation);


        Task<IEnumerable<ProjectGridLocation>> GetAllProjectGridLocation();

        Task<IEnumerable<ProjectGridLocation>> GetAllProjectGridLocationsByProjectId(int levelId);

        Task<ProjectGridLocation> GetProjectGridLocationById(int id);


        #endregion

        #region Location wise activity
        Task<int> SaveProjectLocationActivityDetails(ProjectLocationActivityDetails activityDetails);
        Task<bool> SaveActivityItemDetails(ActivityWiseItemDetial activityItemDetails);

        Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityByLocationId(int locationId);
        Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityBygridId(int gridId);
        Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityByProjectId(int projectId);
        #endregion

        Task<IEnumerable<ActivityWiseMaterialModel>> GetActivityWiseItemDetialByActivityId(int activityId, int locationId);
        Task<IEnumerable<ActivityWiseProjectLocationModel>> GetAllLocationWithActivityDetails(string userId);
        Task<IEnumerable<ActivityWiseProjectLocationModel>> GetAllocatedProjectNameByUser(string userName);
        #region Project Wise Equipment
        Task<bool> SaveProjectEquipment(List<ProjectWiseEquipment> projectWiseEquipment);
        Task<bool> SaveSingleProjectEquipment(ProjectWiseEquipment projectWiseEquipment);
        Task<IEnumerable<ProjectWiseEquipment>> GetProjectEquipmentByProjectId(int projectId);
        Task<IEnumerable<ProjectWiseEquipment>> GetAllProjectEquipment();

        #endregion
    }
}
