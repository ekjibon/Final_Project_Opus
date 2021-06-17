using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface ISectorService
    {
      
        Task<bool> SaveSector(Sector sector);
        Task<IEnumerable<Sector>> GetAllSector();
        Task<Sector> GetSectorById(int id);
        Task<int> GetRootId(int currentID);
        Task<bool> DeleteSectorById(int id);      
        Task<IEnumerable<Sector>> GetSectorByParrentId(int ParentID);
      
    }
}
