using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramImpactService : IProgramImpactService
    {
        private readonly ERPDbContext _context;

        public ProgramImpactService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramImpact(ProgramImpact programCategory)
        {
            if (programCategory.Id != 0)
            {
                programCategory.updatedBy = programCategory.createdBy;
                programCategory.updatedAt = DateTime.Now;
                _context.programImpacts.Update(programCategory);
            }
            else
            {
                programCategory.createdAt = DateTime.Now;
                _context.programImpacts.Add(programCategory);
            }
            await _context.SaveChangesAsync();
            return programCategory.Id;
        }

        public async Task<IEnumerable<ProgramImpact>> GetProgramImpact()
        {
            return await _context.programImpacts.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramImpact> GetProgramImpactById(int id)
        {
            return await _context.programImpacts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
       

        public async Task<bool> DeleteprogramImpactById(int id)
        {
            _context.programImpacts.Remove(_context.programImpacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #region programstatus
        public async Task<int> SaveProgramStatus(ProgramStatus programCategory)
        {
            if (programCategory.Id != 0)
            {
                programCategory.updatedBy = programCategory.createdBy;
                programCategory.updatedAt = DateTime.Now;
                _context.programStatuses.Update(programCategory);
            }
            else
            {
                programCategory.createdAt = DateTime.Now;
                _context.programStatuses.Add(programCategory);
            }
            await _context.SaveChangesAsync();
            return programCategory.Id;
        }

        public async Task<IEnumerable<ProgramStatus>> GetProgramStatus()
        {
            return await _context.programStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramStatus> GetProgramStatusById(int id)
        {
            return await _context.programStatuses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteprogramStatusById(int id)
        {
            _context.programStatuses.Remove(_context.programStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
