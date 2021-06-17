using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IInterestedAreaService
    {
        Task<int> SaveInterestedArea(InterestedArea interestedArea);
        Task<IEnumerable<InterestedArea>> GetInterestedArea();
        Task<InterestedArea> GetInterestedAreaById(int id);
        Task<IEnumerable<InterestedArea>> GetInterestedAreaByRegId(int regId);
        Task<bool> DeleteInterestedAreaById(int id);
    }
}
