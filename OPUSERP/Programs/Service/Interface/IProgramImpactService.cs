using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramImpactService
    {
        Task<int> SaveProgramImpact(ProgramImpact programCategory);
        Task<IEnumerable<ProgramImpact>> GetProgramImpact();
        Task<ProgramImpact> GetProgramImpactById(int id);

        Task<bool> DeleteprogramImpactById(int id);
        #region programstatus
        Task<int> SaveProgramStatus(ProgramStatus programCategory);
        Task<IEnumerable<ProgramStatus>> GetProgramStatus();
        Task<ProgramStatus> GetProgramStatusById(int id);
        Task<bool> DeleteprogramStatusById(int id);
        #endregion
    }
}
