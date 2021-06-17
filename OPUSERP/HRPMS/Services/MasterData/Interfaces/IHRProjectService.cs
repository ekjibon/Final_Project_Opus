using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface IHRProjectService
    {
        Task<bool> SaveHRProject(HRProject hRProject);
        Task<IEnumerable<HRProject>> GetHRProject();
        Task<HRProject> GetHRProjectById(int id);
        Task<bool> DeleteHRProjectById(int id);
    }
}
