using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;

namespace CLUB.Services.MasterData.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> SaveOrganization(Organization orga);
        Task<IEnumerable<Organization>> GetAllOrganization();
        Task<Organization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationById(int id);
        //for validation
        Task<int> GetOrganizationCheck(int id, string name);
    }
}
