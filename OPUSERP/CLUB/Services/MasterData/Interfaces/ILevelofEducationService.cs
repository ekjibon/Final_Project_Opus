using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface ILevelofEducationService
    {
        Task<bool> SaveLevelofEducation(LevelofEducation loe);
        Task<IEnumerable<LevelofEducation>> GetAllLevelofEducation();
        Task<LevelofEducation> GetLevelofEducationById(int id);
        Task<bool> DeleteLevelofEducationById(int id);

        //for validation
        Task<int> GetLevelofEducationCheck(int id, string name);
    }
}
