using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class HRProjectService: IHRProjectService
    {
        private readonly ERPDbContext _context;

        public HRProjectService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHRProject(HRProject hRProject)
        {
            if (hRProject.Id != 0)
                _context.hRProjects.Update(hRProject);
            else
                _context.hRProjects.Add(hRProject);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRProject>> GetHRProject()
        {
            return await _context.hRProjects?.AsNoTracking().ToListAsync();
        }

        public async Task<HRProject> GetHRProjectById(int id)
        {

            return await _context.hRProjects.FindAsync(id);
        }

        public async Task<bool> DeleteHRProjectById(int id)
        {
            _context.hRProjects.Remove(_context.hRProjects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
