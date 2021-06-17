using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;

namespace CLUB.Services.MasterData
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

        public async Task<int> GetSubjectCheck(int id, string name)
        {
            var Subject = await _context.subjects.Where(x => x.subjectName == name && x.Id != id).FirstOrDefaultAsync();
            if (Subject == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            return await _context.subjects.FindAsync(id);
        }

        public async Task<bool> DeleteSubjectById(int id)
        {
            _context.subjects.Remove(_context.subjects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
