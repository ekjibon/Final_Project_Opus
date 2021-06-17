using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OPUSERP.API.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using OPUSERP.SCM.Services.ProjectStatus.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.ProjectStatus
{
    public class ProjcetStatusService: IProjectStatusService
    {
        private readonly ERPDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public ProjcetStatusService(ERPDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        #region Daily Report
        public async Task<int> SaveDailyProgressReport(DailyProgressReport dailyProgress)
        {
            if (dailyProgress.Id != 0)
            {
                _context.DailyProgressReports.Update(dailyProgress);
            }
            else
            {
                _context.DailyProgressReports.Add(dailyProgress);
            }

            await _context.SaveChangesAsync();
            return dailyProgress.Id;
        }

        public async Task<DailyProgressReport> GetDailyProgressReportsById(int id)
        {
            //var result = await (from m in _context.DailyProgressReports.Include(x => x.ApplicationUser).Include(x => x.project)
            //                    join e in _context.employeeInfos on m.ApplicationUserId equals e.ApplicationUserId
            //                    join p in _context.Projects on m.projectId equals p.Id
            //                    where m.Id==id
            //                    select new DailyProgressReport
            //                    {
            //                        Id = m.Id,
            //                        remarks = m.remarks,
            //                        reportDate = m.reportDate,
            //                        reportNo = m.reportNo,
            //                        siteCondition = m.siteCondition,
            //                        siteWeather = m.siteWeather,
            //                        statusId = m.statusId,
            //                        employeeInfo = e,
            //                        project = p
            //                    }).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return await _context.DailyProgressReports.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalReport(int projectId)
        {
            return await _context.DailyProgressReports.Where(x=>x.projectId==projectId).CountAsync();
        }

        public async Task<IEnumerable<DailyProgressReport>> GetDailyProgressReportList(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userid = user.userId;

            var result = await (from m in _context.DailyProgressReports.Include(x=>x.ApplicationUser).Include(x=>x.project)
                                where m.ApplicationUserId==user.Id
                                select new DailyProgressReport
                                {
                                   Id = m.Id,
                                   remarks=m.remarks,
                                   reportDate=m.reportDate,
                                   reportNo=m.reportNo,
                                   statusId=m.statusId,
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DailyProgressReport>> GetDailyProgressReportByFiltering(DateTime? fromDate,DateTime? toDate,int projectId)
        {
            var result = await _context.DailyProgressReports
                .Include(x => x.project)
                .Include(x => x.ApplicationUser)
                .Where(x => x.reportDate >= fromDate && x.reportDate <= toDate && x.projectId == (projectId != 0 ? projectId : x.projectId))
                .AsNoTracking().ToListAsync();
                                //where Convert.ToDateTime(d.reportDate).Date >= Convert.ToDateTime(fromDate).Date
                                //&& Convert.ToDateTime(d.reportDate).Date <= Convert.ToDateTime(toDate).Date
                                //&& d.projectId == (projectId!=0?projectId:d.projectId)
                                //select d).AsNoTracking().ToListAsync();
            
            return result;
        }

        public async Task<DailyProgressReport> GetDailyProgressReportByDay(string date,string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userid = user.userId;

            var result = await (from m in _context.DailyProgressReports.Include(x => x.ApplicationUser).Include(x => x.project)
                                join e in _context.employeeInfos on m.ApplicationUserId equals e.ApplicationUserId
                                join p in _context.Projects on m.projectId equals p.Id
                                where m.ApplicationUserId == user.Id && Convert.ToDateTime(m.reportDate).Date==Convert.ToDateTime(date).Date
                                select new DailyProgressReport
                                {
                                    Id = m.Id,
                                    remarks = m.remarks,
                                    reportDate = m.reportDate,
                                    reportNo = m.reportNo,
                                    siteCondition=m.siteCondition,
                                    siteWeather=m.siteWeather,
                                    statusId = m.statusId,
                                    employeeInfo = e,
                                    project=p
                                }).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Work Progress Activity
        public async Task<int> SaveWorkProgressActivityDescriptionSingle(WorkProgressActivityDescription workProgressActivity)
        {
            if (workProgressActivity.Id != 0)
            {
                _context.WorkProgressActivityDescriptions.Update(workProgressActivity);
            }
            else
            {
                _context.WorkProgressActivityDescriptions.Add(workProgressActivity);
            }

            await _context.SaveChangesAsync();
            return workProgressActivity.Id;
        }
        public async Task<bool> SaveWorkProgressActivityDescription(List<WorkProgressActivityDescription> workProgress)
        {
            
            await _context.WorkProgressActivityDescriptions.AddRangeAsync(workProgress);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<WorkProgressActivityType>> GetWorkProgressActivityType()
        {
            return await _context.WorkProgressActivityTypes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WorkProgressActivityDescription>> GetWorkProgressActivityTypeByReportId(int reportId)
        {
            return await _context.WorkProgressActivityDescriptions.Include(x=>x.progressActivityType).Where(x=>x.progressReportId==reportId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveWorkProgressActivityType(WorkProgressActivityType workProgressActivityType)
        {
            if (workProgressActivityType.Id != 0)
                _context.WorkProgressActivityTypes.Update(workProgressActivityType);
            else
                _context.WorkProgressActivityTypes.Add(workProgressActivityType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveActivityProgressItemDetails(List<ActivityWiseDailyProgressDetail> workProgress)
        {

            await _context.ActivityWiseDailyProgressDetails.AddRangeAsync(workProgress);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ActivityWiseDailyProgressDetail>> GetActivityProgressItemDetailsByActivityId(int activityId)
        {
            return await _context.ActivityWiseDailyProgressDetails.Where(x=>x.workProgressActivityId==activityId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ActivityWiseDailyProgressDetail>> GetActivityProgressItemDetailsByReportId(int reportId)
        {
            return await _context.ActivityWiseDailyProgressDetails.Include(x=>x.itemDetial.itemSpecification).Where(x => x.workProgressActivity.progressReportId == reportId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Site Equipment
        public async Task<bool> SaveSiteEquipment(List<SiteEquipment> siteEquipment)
        {
            await _context.SiteEquipment.AddRangeAsync(siteEquipment);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SiteEquipment>> GetSiteEquipmentByReportId(int reportId)
        {
            return await _context.SiteEquipment.Include(x => x.itemSpecification).Where(x=>x.progressReportId==reportId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Site Material
        public async Task<bool> SaveSiteMaterial(List<SiteMaterial> siteMaterial)
        {
            await _context.SiteMaterials.AddRangeAsync(siteMaterial);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Site Manpower
        public async Task<bool> SaveSiteManpower(List<SiteManpower> siteManpower)
        {
            await _context.SiteManpowers.AddRangeAsync(siteManpower);
            
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<SiteManpower>> GetSiteManpowerByProgressId(int progressId)
        {
            return await _context.SiteManpowers.Where(x => x.progressReportId == progressId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Site Site Visitors
        public async Task<bool> SaveSiteVisitor(List<SiteVisitor> siteVisitor)
        {
            await _context.SiteVisitors.AddRangeAsync(siteVisitor);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public void UpdateVisitorById(int id, string imagePath)
        {
            var gD = _context.SiteVisitors.Where(x => x.Id == id).FirstOrDefault();
            gD.imagePath = imagePath;
            _context.Entry(gD).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<SiteVisitor>> GetSiteVisitorByProgressId(int progressId)
        {
            return await _context.SiteVisitors.Where(x => x.progressReportId == progressId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Site Site Constraints
        public async Task<bool> SaveSiteConstraints(List<SiteConstraint> siteConstraint)
        {
            await  _context.SiteConstraints.AddRangeAsync(siteConstraint);
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SiteConstraint>> GetSiteConstraintByProgressId(int progressId)
        {
            return await _context.SiteConstraints.Where(x => x.progressReportId == progressId).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Location
        public async Task<int> SaveProjectLocation(ProjectConstructionLocation projectConstructionLocation)
        {
            if (projectConstructionLocation.Id != 0)
                _context.ProjectConstructionLocations.Update(projectConstructionLocation);
            else
                _context.ProjectConstructionLocations.Add(projectConstructionLocation);
            await _context.SaveChangesAsync();
            //return 1 == await _context.SaveChangesAsync();
            return projectConstructionLocation.Id;
        }

        public async Task<IEnumerable<ProjectConstructionLocation>> GetAllProjectLocation()
        {
            return await _context.ProjectConstructionLocations.Include(x=>x.project).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ProjectConstructionLocation>> GetAllProjectLocationByProjectId(int projectId,string userName)
        {
            var user = _context.Users.Where(x => x.UserName == userName).FirstOrDefault();
            var role = _context.UserRoles.Where(x => x.RoleId == user.Id).FirstOrDefault();
            return await _context.ProjectConstructionLocations.Include(x => x.project).Where(x => x.projectId == projectId && x.ApplicationUser.UserName== (role.RoleId != "5be51169-1af2-4d34-add1-469ed7aed1b1" ? userName : x.ApplicationUser.UserName)).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProjectConstructionLocation> GetProjectLocationById(int id)
        {
            return await _context.ProjectConstructionLocations.FindAsync(id);
        }

        //public async Task<bool> DeleteProjectLocationById(int id)
        //{
        //    _context.ProjectConstructionLocations.Remove(_context.ProjectConstructionLocations.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

        #endregion

        #region Grid Location
        public async Task<int> SaveProjectGridLocation(ProjectGridLocation projectGridLocation)
        {
            if (projectGridLocation.Id != 0)
                _context.ProjectGridLocations.Update(projectGridLocation);
            else
                _context.ProjectGridLocations.Add(projectGridLocation);
            await _context.SaveChangesAsync();
            //return 1 == await _context.SaveChangesAsync();
            return projectGridLocation.Id;
        }

        public async Task<IEnumerable<ProjectGridLocation>> GetAllProjectGridLocation()
        {
            return await _context.ProjectGridLocations.Include(x => x.projectConstructionLocation.project).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ProjectGridLocation>> GetAllProjectGridLocationsByProjectId(int levelId)
        {
            return await _context.ProjectGridLocations.Include(x => x.projectConstructionLocation).Where(x => x.projectConstructionLocationId == levelId).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProjectGridLocation> GetProjectGridLocationById(int id)
        {
            return await _context.ProjectGridLocations.FindAsync(id);
        }

        //public async Task<bool> DeleteProjectLocationById(int id)
        //{
        //    _context.ProjectConstructionLocations.Remove(_context.ProjectConstructionLocations.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

        #endregion

        #region Location wise activity
        public async Task<int> SaveProjectLocationActivityDetails(ProjectLocationActivityDetails activityDetails)
        {
            if (activityDetails.Id != 0)
                _context.ProjectLocationActivityDetails.Update(activityDetails);
            else
                _context.ProjectLocationActivityDetails.Add(activityDetails);
            await _context.SaveChangesAsync();
            return activityDetails.Id;
        }

        public async Task<bool> SaveActivityItemDetails(ActivityWiseItemDetial activityItemDetails)
        {
            if (activityItemDetails.Id != 0)
                _context.ActivityWiseItemDetials.Update(activityItemDetails);
            else
                _context.ActivityWiseItemDetials.Add(activityItemDetails);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityByLocationId(int locationId)
        {
            return await _context.ProjectLocationActivityDetails.Include(x=>x.progressActivityType).Include(x => x.projectGridLocation.projectConstructionLocation.project).Where(x=>x.projectGridLocation.projectConstructionLocationId == locationId).ToListAsync();
        }

        public async Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityBygridId(int gridId)
        {
            var result= _context.ProjectLocationActivityDetails.Include(x => x.progressActivityType).Include(x=>x.unit).Where(x => x.projectGridLocationId == gridId).ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<ProjectLocationActivityDetails>> GetAllProjectLocationActivityByProjectId(int projectId)
        {
            return await _context.ProjectLocationActivityDetails.Include(x => x.progressActivityType).Include(x=>x.projectGridLocation.projectConstructionLocation.project).Where(x => x.projectGridLocation.projectConstructionLocation.projectId == projectId).ToListAsync();
        }

        public async Task<IEnumerable<ActivityWiseProjectLocationModel>> GetAllLocationWithActivityDetails(string userId)
        {
            var result = await _context.activityWiseProjectLocationModels.FromSql($"SP_GET_Project_Locatio_List {userId}").ToListAsync();
            //var result =await (from p in _context.Projects
            //             join pcl in _context.ProjectConstructionLocations on p.specialBranchUnitId equals pcl.projectId
            //             join plad in (from pd in _context.ProjectLocationActivityDetails
            //                           join wpat in _context.WorkProgressActivityTypes on pd.progressActivityTypeId equals wpat.Id
            //                           join au in _context.Units on pd.unitId equals au.Id
            //                           group new { pd, wpat, au } by new { pd.constructionLocationId, pd.Id, wpat.activityName, au.unitName } into pdd
            //                           select new { constructionLocationId = pdd.Key.constructionLocationId, Id = pdd.Key.Id, activityName = pdd.Key.activityName, unitName = pdd.Key.unitName,qty=pdd.Sum(x=>x.pd.qty) }) on pcl.Id equals plad.constructionLocationId
            //             join awid in (from ad in _context.ActivityWiseItemDetials
            //                           join isp in _context.ItemSpecifications on ad.itemSpecificationId equals isp.Id
            //                           join i in _context.Items on isp.itemId equals i.Id
            //                           join iu in _context.Units on ad.unitId equals iu.Id
            //                           group new { ad} by new { ad.activityDetailsId } into aadd
            //                           select new { activityDetailsId = aadd.Key.activityDetailsId, totalItem = aadd.Count(), itemWiseTotalQTY = aadd.Sum(x => x.ad.qty) }) on plad.Id equals awid.activityDetailsId
            //             join a in _context.Users on pcl.ApplicationUserId equals a.Id
            //             join e in _context.employeeInfos on a.Id equals e.ApplicationUserId
            //             select new ActivityWiseProjectLocationModel
            //             {
            //                 projectId=p.Id,
            //                 reportingUser=e.nameEnglish,
            //                 activityName=plad.activityName,
            //                 itemWiseTotalQTY=awid.itemWiseTotalQTY,
            //                 locationPosition=pcl.locationPosition,
            //                 projectLocationId=pcl.Id,
            //                 projectShortName=p.projectShortName,
            //                 qty=plad.qty,
            //                 totalItem=awid.totalItem,
            //                 unitName=plad.unitName,
            //                 userName=a.UserName
            //             }).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ActivityWiseProjectLocationModel>> GetAllocatedProjectNameByUser(string userName)
        {
            var user =await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            var role =await _context.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            var result = new List<ActivityWiseProjectLocationModel>();
            if (role.RoleId == "5be51169-1af2-4d34-add1-469ed7aed1b1")
            {
                result = await (from p in _context.Projects
                                select new ActivityWiseProjectLocationModel
                                {
                                    projectId = p.Id,
                                    projectShortName = p.projectName,
                                    userName=""
                                }).ToListAsync();
            }
            else
            {
                result = await (from p in _context.Projects
                                join pcl in _context.ProjectConstructionLocations on p.Id equals pcl.projectId
                                join a in _context.Users.Where(x => x.UserName == userName) on pcl.ApplicationUserId equals a.Id
                                group new { p, a } by new { p.Id, p.projectName, a.UserName } into pp
                                select new ActivityWiseProjectLocationModel
                                {
                                    projectId = pp.Key.Id,
                                    projectShortName = pp.Key.projectName,
                                    userName = pp.Key.UserName
                                }).ToListAsync();
            }


            return result;
        }

        #endregion

        #region Activity Wise Item Detail

        public async Task<IEnumerable<ActivityWiseMaterialModel>> GetActivityWiseItemDetialByActivityId(int activityId, int locationId)
        {
            var result = await (from a in _context.ActivityWiseItemDetials
                                join d in _context.ProjectLocationActivityDetails.Include(x => x.progressActivityType) on a.activityDetailsId equals d.Id
                                join s in _context.ItemSpecifications on a.itemSpecificationId equals s.Id
                                where d.progressActivityTypeId == activityId && d.projectGridLocationId == locationId
                                select new ActivityWiseMaterialModel
                                {
                                    activityName = d.progressActivityType.activityName,
                                    Id = a.Id,
                                    materialId = s.Id,
                                    materialName = s.specificationName,
                                    activityDetailsId = d.Id
                                }).ToListAsync();
            return result;
        }



        #endregion

        #region Project Wise Equipment
        public async Task<bool> SaveProjectEquipment(List<ProjectWiseEquipment> projectWiseEquipment)
        {
            await _context.ProjectWiseEquipment.AddRangeAsync(projectWiseEquipment);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SaveSingleProjectEquipment(ProjectWiseEquipment projectWiseEquipment)
        {
            await _context.ProjectWiseEquipment.AddAsync(projectWiseEquipment);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectWiseEquipment>> GetProjectEquipmentByProjectId(int projectId)
        {
            return await _context.ProjectWiseEquipment.Include(x => x.itemSpecification).Include(x=>x.project).Where(x => x.projectId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ProjectWiseEquipment>> GetAllProjectEquipment()
        {
            return await _context.ProjectWiseEquipment.Include(x => x.itemSpecification).Include(x => x.project).AsNoTracking().ToListAsync();
        }

        #endregion

    }
}
