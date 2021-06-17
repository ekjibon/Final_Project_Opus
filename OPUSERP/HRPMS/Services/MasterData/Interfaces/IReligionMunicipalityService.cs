using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IReligionMunicipalityService
    {
        Task<bool> SaveReligion(Religion religion);
        Task<IEnumerable<Religion>> GetReligions();
        Task<Religion> GetReligionById(int id);
        Task<bool> DeleteReligionById(int id);

        Task<bool> SaveMunicipilityLocation(MunicipilityLocation municipilityLocation);
        Task<IEnumerable<MunicipilityLocation>> GetMunicipilityLocation();
        Task<MunicipilityLocation> GetMunicipilityLocationById(int id);
        Task<bool> DeleteMunicipilityLocationById(int id);
    }
}
