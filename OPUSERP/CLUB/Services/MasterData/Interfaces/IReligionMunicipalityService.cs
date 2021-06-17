using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IReligionMunicipalityService
    {
        Task<bool> SaveReligion(Religion religion);
        Task<IEnumerable<Religion>> GetReligions();
        Task<Religion> GetReligionById(int id);
        Task<bool> DeleteReligionById(int id);
        //for validation
        Task<int> GetReligionCheck(int id, string name);

        Task<bool> SaveMunicipilityLocation(MunicipilityLocation municipilityLocation);
        Task<IEnumerable<MunicipilityLocation>> GetMunicipilityLocation();
        Task<MunicipilityLocation> GetMunicipilityLocationById(int id);
        Task<bool> DeleteMunicipilityLocationById(int id);
    }
}
