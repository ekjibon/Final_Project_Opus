using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IOccupationCadreService
    {
        Task<bool> SaveOccupationInfo(Occupation occupation);
        Task<IEnumerable<Occupation>> GetOccupationInfo();
        Task<Occupation> GetOccupationInfoById(int id);
        Task<bool> DeleteOccupationInfoById(int id);

        Task<bool> SaveCadreInfo(Cadre vehicle);
        Task<IEnumerable<Cadre>> GetCadreInfo();
        Task<Cadre> GetCadreInfoById(int id);
        Task<bool> DeleteCadreInfoById(int id);
    }
}
