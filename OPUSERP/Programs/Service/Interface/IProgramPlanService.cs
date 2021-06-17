using OPUSERP.Areas.Program.Models;
using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramPlanService
    {
        #region programPlan
        Task<int> SaveprogramPlan(programPlan programPlan);
        Task<IEnumerable<programPlan>> GetprogramPlan();
        Task<programPlan> GetprogramPlanById(int id);
        Task<bool> DeleteprogramPlanById(int id);
        #endregion

        #region Program Work Plan

        Task<int> SaveProgramWorkPlan(ProgramWorkPlan programWorkPlan);
        Task<ProgramWorkPlan> GetProgramWorkPlanById(int? yearId, int? masterId);
        Task<IEnumerable<ProgramWorkPlan>> GetProgramWorkPlanByMasterYearId(int? yearId, int? masterId);
        Task<bool> DeleteProgramWorkPlansByMasterYearId(int? yearId, int? masterId);
        Task<IEnumerable<ProgramPlanListViewModel>> GetProjectPlanList();
        #endregion

        #region ProgramPlan Location

        Task<int> SaveProgramPlanLocation(ProgramPlanLocation programPlanLocation);
        Task<bool> UpdateProgramPlanLocation(int Id, decimal? amount);
        Task<IEnumerable<ProgramPlanLocation>> GetProgramPlanLocationByActivityMonthYearId(int? yearId, int? activityId, string month);
        Task<bool> DeleteProgramPlanLocationById(int? Id);

        #endregion

        #region Program Plan Report

        Task<IEnumerable<ProgramPlanReportViewModel>> GetProgramPlanReport(int? masterId, int? yearId);      

        #endregion
    }
}
