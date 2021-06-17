using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VEMS.Services.Registration.Interface
{
    public interface IAuthorizedPersonService
    {
        Task<int> SaveAuthorizedPerson(AuthorizedPerson authorizedPerson);
        Task<IEnumerable<AuthorizedPerson>> GetAuthorizedPerson();
        Task<AuthorizedPerson> GetAuthorizedPersonById(int id);
        Task<IEnumerable<AuthorizedPerson>> GetAuthorizedPersonByRegId(int regId);
        Task<bool> DeleteAuthorizedPersonById(int id);
    }
}
