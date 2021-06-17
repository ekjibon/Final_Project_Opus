using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier.Interface
{
  public interface IContactService
    {
        Task<int> SaveContact(Contact contact);
        Task<IEnumerable<Contact>> GetContact();
        Task<Contact> GetContactById(int id);
        Task<bool> DeleteContactById(int id);
        Task<IEnumerable<Contact>> GetContactbyOrganizationId(int Id);
        Task<IEnumerable<Contact>> GetcontactbyOrganizationId(int Id);
        Task<bool> DeleteContactByOrganizationId(int id);
    }
}
