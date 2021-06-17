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
    public class ProgramPeopleInDiscussionService : IProgramPeopleInDiscussionService
    {
        private readonly ERPDbContext _context;

        public ProgramPeopleInDiscussionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramPeopleInDiscussion(ProgramPeopleInDiscussion  programPeopleInDiscussion)
        {
            if (programPeopleInDiscussion.Id != 0)
            {
                programPeopleInDiscussion.updatedBy = programPeopleInDiscussion.createdBy;
                programPeopleInDiscussion.updatedAt = DateTime.Now;
                _context.programPeopleInDiscussions.Update(programPeopleInDiscussion);
            }
            else
            {
                programPeopleInDiscussion.createdAt = DateTime.Now;
                _context.programPeopleInDiscussions.Add(programPeopleInDiscussion);
            }
            await _context.SaveChangesAsync();
            return programPeopleInDiscussion.Id;
        }

        public async Task<IEnumerable<ProgramPeopleInDiscussion>> GetProgramPeopleInDiscussion()
        {
            return await _context.programPeopleInDiscussions.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramPeopleInDiscussion> GetProgramPeopleInDiscussionById(int id)
        {
            return await _context.programPeopleInDiscussions.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramPeopleInDiscussionById(int id)
        {
            _context.programPeopleInDiscussions.Remove(_context.programPeopleInDiscussions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
