using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData.Interfaces
{
    public interface ICompanyGroupService
    {
        // Company Group
        Task<bool> SaveCompanyGroup(CompanyGroup companyGroup);
        Task<IEnumerable<CompanyGroup>> GetAllCompanyGroup();
        Task<CompanyGroup> GetCompanyGroupById(int id);
        Task<bool> DeleteCompanyGroupById(int id);
    }
}
