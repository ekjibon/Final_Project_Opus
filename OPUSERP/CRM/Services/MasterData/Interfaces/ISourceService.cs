using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface ISourceService
    {
        // Source
        Task<bool> SaveSource(Source source);
        Task<IEnumerable<Source>> GetAllSource();
        Task<Source> GetSourceById(int id);
        Task<bool> DeleteSourceById(int id);
    }
}
