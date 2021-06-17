using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramYearService
    {
        Task<int> SaveProgramYear(ProgramYear programYear);
        Task<IEnumerable<ProgramYear>> GetProgramYear();
        Task<ProgramYear> GetProgramYearById(int id);
        Task<bool> DeleteProgramYearById(int id);
    }
}
