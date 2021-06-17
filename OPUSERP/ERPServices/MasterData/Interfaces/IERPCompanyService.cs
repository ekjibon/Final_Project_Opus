using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface IERPCompanyService
    {
        Task<int> SaveERPCompany(Company company);

        void UpdateCompanyLogoById(int compId, string fileName, string fileLocation);

        Task<IEnumerable<Company>> GetAllCompany();

        Task<Company> GetCompanyById(int id);

        Task<bool> DeleteCompanyById(int id);
    }
}
