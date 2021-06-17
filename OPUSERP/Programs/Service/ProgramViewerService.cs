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
    public class ProgramViewerService : IProgramViewerService
    {
        private readonly ERPDbContext _context;

        public ProgramViewerService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramViewer(ProgramViewer  programViewer)
        {
            if (programViewer.Id != 0)
            {
                programViewer.updatedBy = programViewer.createdBy;
                programViewer.updatedAt = DateTime.Now;
                _context.programViewers.Update(programViewer);
            }
            else
            {
                programViewer.createdAt = DateTime.Now;
                _context.programViewers.Add(programViewer);
            }
            await _context.SaveChangesAsync();
            return programViewer.Id;
        }

        public async Task<IEnumerable<ProgramViewer>> GetProgramViewer()
        {
            return await _context.programViewers.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramViewer> GetProgramViewerById(int id)
        {
            return await _context.programViewers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramViewerById(int id)
        {
            _context.programViewers.Remove(_context.programViewers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
