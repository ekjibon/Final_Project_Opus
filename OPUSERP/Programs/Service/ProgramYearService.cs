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
    public class ProgramYearService: IProgramYearService
    {
        private readonly ERPDbContext _context;

        public ProgramYearService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramYear(ProgramYear programYear)
        {
            if (programYear.Id != 0)
            {
                programYear.updatedBy = programYear.createdBy;
                programYear.updatedAt = DateTime.Now;
                _context.programYears.Update(programYear);
            }
            else
            {
                programYear.createdAt = DateTime.Now;
                _context.programYears.Add(programYear);
            }
            await _context.SaveChangesAsync();
            return programYear.Id;
        }

        public async Task<IEnumerable<ProgramYear>> GetProgramYear()
        {
            return await _context.programYears.AsNoTracking().OrderBy(a => a.name).ToListAsync();
        }

        public async Task<ProgramYear> GetProgramYearById(int id)
        {
            return await _context.programYears.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramYearById(int id)
        {
            _context.programYears.Remove(_context.programYears.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
