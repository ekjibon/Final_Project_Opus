using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramSubjectService
    {
        Task<int> SaveProgramSubject(ProgramSubject  programSubject);
        Task<IEnumerable<ProgramSubject>> GetProgramSubject();
        Task<ProgramSubject> GetProgramSubjectById(int id);
        Task<bool> DeleteProgramSubjectById(int id);
    }
}
