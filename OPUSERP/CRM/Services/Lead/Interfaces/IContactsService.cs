using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IContactsService
    {
        Task<int> SaveContacts(Contacts Contacts); 
         Task<IEnumerable<Contacts>> GetAllContacts();
        Task<IEnumerable<Contacts>> GetAllContactsbyLeadId(int Id);
        Task<IEnumerable<Contacts>> GetOwnContactsbyLeadId(int Id);
        Task<Contacts> GetContactsById(int id);
        Task<bool> DeleteContactsById(int id);
        Task<IEnumerable<Contacts>> GetAllContactsByLeadsId(int leadId);
        Task<IEnumerable<Contacts>> GetAllContactsByLeadsIdforPersonal(int leadId);
        Task<IEnumerable<ContactsSpViewModel>> GetContactsSpViewModel(string TeamLeader, int Fa, string BD, string LeadId);
        Task<IEnumerable<BankContactsSpViewModel>> GetBankContactsSpViewModel(string TeamLeader, int Fa, string BD, string LeadId, int bankId, int branchId);
    }
}
