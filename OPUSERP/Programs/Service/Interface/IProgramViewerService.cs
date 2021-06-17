using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
    public interface IProgramViewerService
    {
        Task<int> SaveProgramViewer(ProgramViewer  programViewer);
        Task<IEnumerable<ProgramViewer>> GetProgramViewer();
        Task<ProgramViewer> GetProgramViewerById(int id);
        Task<bool> DeleteProgramViewerById(int id);
    }
}
