using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IOccupationCadreService
    {
        Task<bool> SaveOccupationInfo(Occupation occupation);
        Task<IEnumerable<Occupation>> GetOccupationInfo();
        Task<Occupation> GetOccupationInfoById(int id);
        Task<bool> DeleteOccupationInfoById(int id);
        //for validation
        Task<int> GetOccupationCheck(int id, string name);

        Task<bool> SaveCadreInfo(Cadre vehicle);
        Task<IEnumerable<Cadre>> GetCadreInfo();
        Task<Cadre> GetCadreInfoById(int id);
        Task<bool> DeleteCadreInfoById(int id);
    }
}
