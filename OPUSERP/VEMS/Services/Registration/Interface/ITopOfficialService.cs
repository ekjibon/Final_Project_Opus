using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface ITopOfficialService
    {
        Task<int> SaveTopOfficial(TopOfficial topOfficial);
        Task<IEnumerable<TopOfficial>> GetTopOfficial();
        Task<TopOfficial> GetTopOfficialById(int id);
        Task<IEnumerable<TopOfficial>> GetTopOfficialByRegId(int regId);
        Task<bool> DeleteTopOfficialById(int id);
    }
}
