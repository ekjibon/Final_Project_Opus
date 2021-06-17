using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CLUB.Services.MasterData
{
    public class RelDegreeSubjectService : IRelDegreeSubjectService
    {
        private readonly ERPDbContext _context;

        public RelDegreeSubjectService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDegreeSubject(RelDegreeSubject relDegreeSubject)
        {
            if(relDegreeSubject.Id !=0 )
                _context.relDegreeSubjects.Update(relDegreeSubject);
            else
                _context.relDegreeSubjects.Add(relDegreeSubject);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetdegreeSubjectCheck(int SubjectId, int degreeId)
        {
            var Result = await _context.relDegreeSubjects.Where(x => x.subjectId == SubjectId && x.degreeId == degreeId).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<RelDegreeSubject>> GetAllDegreeSubject()
        {
            return await _context.relDegreeSubjects.Include(x=>x.degree).Include(x => x.subject).AsNoTracking().ToListAsync();
        }

        public async Task<RelDegreeSubject> GetDegreeSubjectById(int id)
        {
            return await _context.relDegreeSubjects.FindAsync(id);
        }

        public async Task<bool> DeleteDegreeSubjectById(int id)
        {
            _context.relDegreeSubjects.Remove(_context.relDegreeSubjects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelDegreeSubject>> GetSubjectByDegreeId(int DegId)
        {
            return await _context.relDegreeSubjects.Where(x => x.degreeId == DegId).Include(x => x.subject).ToListAsync();
        }
    }
}
