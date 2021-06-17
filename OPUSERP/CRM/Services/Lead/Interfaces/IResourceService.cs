using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.SPModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IResourceService
    {
        Task<bool> SaveResource(Resource Resource);
        Task<IEnumerable<Resource>> GetAllResource();
        Task<Resource> GetResourceById(int id);
        Task<bool> DeleteResourceById(int id);
        Task<int> SaveResourceReturnId(Resource Resource);
        Task<IEnumerable<Resource>> GetTotalContact();
    }
}
