using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ILevelofEducationService
    {
        Task<bool> SaveLevelofEducation(LevelofEducation loe);
        Task<IEnumerable<LevelofEducation>> GetAllLevelofEducation();
        Task<LevelofEducation> GetLevelofEducationById(int id);
        Task<bool> DeleteLevelofEducationById(int id);
    }
}
