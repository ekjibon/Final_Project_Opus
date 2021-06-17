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
    public class ProgramSubjectService : IProgramSubjectService
    {
        private readonly ERPDbContext _context;

        public ProgramSubjectService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramSubject(ProgramSubject  programSubject)
        {
            if (programSubject.Id != 0)
            {
                programSubject.updatedBy = programSubject.createdBy;
                programSubject.updatedAt = DateTime.Now;
                _context.programSubjects.Update(programSubject);
            }
            else
            {
                programSubject.createdAt = DateTime.Now;
                _context.programSubjects.Add(programSubject);
            }
            await _context.SaveChangesAsync();
            return programSubject.Id;
        }

        public async Task<IEnumerable<ProgramSubject>> GetProgramSubject()
        {
            return await _context.programSubjects.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramSubject> GetProgramSubjectById(int id)
        {
            return await _context.programSubjects.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramSubjectById(int id)
        {
            _context.programSubjects.Remove(_context.programSubjects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
