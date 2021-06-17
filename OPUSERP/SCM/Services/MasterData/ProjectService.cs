using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData
{
    public class ProjectService: IProjectService
    {
        private readonly ERPDbContext _context;

        public ProjectService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProject(Project project)
        {
            if (project.Id != 0)
            {
                project.updatedBy = project.createdBy;
                project.updatedAt = DateTime.Now;
                _context.Projects.Update(project);
            }
            else
            {
                project.createdAt = DateTime.Now;
                _context.Projects.Add(project);
            }

            await _context.SaveChangesAsync();
            return project.Id;
        }

        public void UpdateProjectStatusById(int projectId, string status)
        {
            var project = _context.Projects.Find(projectId);
            project.projectStatus = status;
            _context.Entry(project).State = EntityState.Modified;
            
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Project>> GetProjectList()
        {
            return await _context.Projects.OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectListByUser(string userName)
        {
            return await _context.Projects.AsNoTracking().ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }
        public async Task<IEnumerable<Project>> GetProjectBySBUId(int id)
        {
            return await _context.Projects.Where(x=>x.specialBranchUnitId==id).Include(x=>x.specialBranchUnit).ToListAsync();
        }

        public async Task<bool> DeleteProjectById(int id)
        {
            _context.Projects.Remove(_context.Projects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
