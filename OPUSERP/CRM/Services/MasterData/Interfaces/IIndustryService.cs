using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IIndustryService
    {
        Task<bool> SaveIndustry(Industry agreementCategory);
        Task<IEnumerable<Industry>> GetAllIndustry();
        Task<Industry> GetIndustryById(int id);
        Task<bool> DeleteIndustriesById(int id);

    }
}
