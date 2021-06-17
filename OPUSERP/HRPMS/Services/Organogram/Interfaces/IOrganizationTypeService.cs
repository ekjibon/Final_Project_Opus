using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Organogram.Interfaces
{
    public interface IOrganizationTypeService
    {
        Task<bool> SaveOrganizationType(OrganizationType organizationType);
        Task<IEnumerable<OrganizationType>> GetAllOrganizationType();
        Task<OrganizationType> GetOrganizationTypeById(int id);
        Task<bool> DeleteOrganizationTypeById(int id);
    }
}
