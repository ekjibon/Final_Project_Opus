using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramPeopleInDiscussionService
    {
        Task<int> SaveProgramPeopleInDiscussion(ProgramPeopleInDiscussion  programPeopleInDiscussion);
        Task<IEnumerable<ProgramPeopleInDiscussion>> GetProgramPeopleInDiscussion();
        Task<ProgramPeopleInDiscussion> GetProgramPeopleInDiscussionById(int id);
        Task<bool> DeleteProgramPeopleInDiscussionById(int id);
    }
}
