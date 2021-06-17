using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace OPUSERP.HRPMS.Services.MasterData
{
    public class SubjectService : ISubjectService
    {
        private readonly ERPDbContext _context;

        public SubjectService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveSubject(Subject subject)
        {
            if(subject.Id != 0)
                _context.subjects.Update(subject);
            else
                _context.subjects.Add(subject);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubject()
        {
            return await _context.subjects.AsNoTracking().ToListAsync();
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            return await _context.subjects.FindAsync(id);
        }

        public async Task<bool> DeleteSubjectById(int id)
        {
            _context.employeeTypes.Remove(_context.employeeTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
