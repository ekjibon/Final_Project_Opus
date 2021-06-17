using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData.Interfaces
{
    public interface IAgreementCategoryService
    {
        Task<bool> SaveAgreementCategory(AgreementCategory agreementCategory);
        Task<IEnumerable<AgreementCategory>> GetAllAgreementCategory();
        Task<AgreementCategory> GetAgreementCategoryById(int id);
        Task<bool> DeleteAgreementCategoryById(int id);

    }
}
