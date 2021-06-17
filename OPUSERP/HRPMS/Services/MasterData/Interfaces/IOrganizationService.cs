using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IOrganizationService
    {
        Task<bool> SaveOrganization(Organization orga);
        Task<IEnumerable<Organization>> GetAllOrganization();
        Task<Organization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationById(int id);
    }
}
