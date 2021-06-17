using OPUSERP.Programs.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service.Interface
{
   public interface IProgramVideoService
    {
        Task<int> SaveProgramVideo(ProgramVideo programVideo);
        Task<IEnumerable<ProgramVideo>> GetProgramVideo();
        Task<ProgramVideo> GetProgramVideoById(int id);
        Task<bool> DeleteProgramVideoById(int id);
    }
}
