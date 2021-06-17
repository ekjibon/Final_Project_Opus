using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramPlanService: IProgramPlanService
    {
        private readonly ERPDbContext _context;

        public ProgramPlanService(ERPDbContext context)
        {
            _context = context;
        }

        #region programPlan

        public async Task<int> SaveprogramPlan(programPlan programPlan)
        {
            if (programPlan.Id != 0)
            {
                programPlan.updatedBy = programPlan.createdBy;
                programPlan.updatedAt = DateTime.Now;
                _context.programPlans.Update(programPlan);
            }
            else
            {
                programPlan.createdAt = DateTime.Now;
                _context.programPlans.Add(programPlan);
            }
            await _context.SaveChangesAsync();
            return programPlan.Id;
        }

        public async Task<IEnumerable<programPlan>> GetprogramPlan()
        {
            return await _context.programPlans.Include(x=>x.district).Include(x => x.division).Include(x => x.programCategory).Include(x => x.programYear).AsNoTracking().ToListAsync();
        }

        public async Task<programPlan> GetprogramPlanById(int id)
        {
            return await _context.programPlans.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteprogramPlanById(int id)
        {
            _context.programPlans.Remove(_context.programPlans.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion Program WorkPlan

        #region Program WorkPlan
        public async Task<int> SaveProgramWorkPlan(ProgramWorkPlan programWorkPlan)
        {
            if (programWorkPlan.Id != 0)
            {
                _context.ProgramWorkPlans.Update(programWorkPlan);
            }
            else
            {
                _context.ProgramWorkPlans.Add(programWorkPlan);
            }
            await _context.SaveChangesAsync();
            return programWorkPlan.Id;
        }

        public async Task<ProgramWorkPlan> GetProgramWorkPlanById(int? yearId, int? masterId)
        {
            return await _context.ProgramWorkPlans.Where(a => a.programYearId == yearId && a.programActivity.programMasterId == masterId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProgramWorkPlan>> GetProgramWorkPlanByMasterYearId(int? yearId, int? masterId)
        {
            return await _context.ProgramWorkPlans.Include(x => x.programActivity).Where(x => x.programYearId == yearId && x.programActivity.programMasterId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteProgramWorkPlansByMasterYearId(int? yearId, int? masterId)
        {
            _context.ProgramWorkPlans.RemoveRange(_context.ProgramWorkPlans.Where(x => x.programYearId == yearId && x.programActivity.programMasterId == masterId));

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProgramPlanListViewModel>> GetProjectPlanList()
        {
            return await _context.programPlanListViewModels.FromSql($"SP_GetProjectPlanList").AsNoTracking().ToListAsync();
        }


        #endregion
               

        #region ProgramPlan Location

        public async Task<int> SaveProgramPlanLocation(ProgramPlanLocation programPlanLocation)
        {
            if (programPlanLocation.Id != 0)
            {
                _context.ProgramPlanLocations.Update(programPlanLocation);
            }
            else
            {
                _context.ProgramPlanLocations.Add(programPlanLocation);
            }
            await _context.SaveChangesAsync();
            return programPlanLocation.Id;
        }

        public async Task<bool> UpdateProgramPlanLocation(int Id, decimal? amount)
        {
            var pLocation = _context.ProgramPlanLocations.Find(Id);
            pLocation.executionQuantity = amount;            
            _context.Entry(pLocation).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProgramPlanLocation>> GetProgramPlanLocationByActivityMonthYearId(int? yearId, int? activityId, string month)
        {
            return await _context.ProgramPlanLocations.Include(x => x.division).Include(x => x.district).Include(x => x.thana).Where(x => x.programYearId == yearId && x.programActivityId == activityId && x.monthName== month).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteProgramPlanLocationById(int? Id)
        {
            _context.ProgramPlanLocations.RemoveRange(_context.ProgramPlanLocations.Where(x => x.Id == Id));

            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Program Plan Report

        public async Task<IEnumerable<ProgramPlanReportViewModel>> GetProgramPlanReport(int? masterId, int? yearId)
        {
            return await _context.programPlanReportViewModels.FromSql($"SP_RPT_ProgramPlanByYear {masterId},{yearId}").AsNoTracking().ToListAsync();
        }


        #endregion
    }
}
