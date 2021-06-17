using OPUSERP.VIMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VIMS.Services.VIMS.Interfaces
{
    public interface IVisitorEntryService
    {
        Task<bool> SaveVisitor(Visitor visitor);
        Task<IEnumerable<Visitor>> GetAllVisitor();
        Task<Visitor> GetVisitorById(int id);
        Task<bool> DeleteVisitorById(int id); 
        Task<IEnumerable<Visitor>> GetContactphotoByContactId(int id);
    }
}
