using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Shagotom.Data.Entity.Visitor;

namespace OPUSERP.Shagotom.Services.Visitor.Interfaces
{
    public interface IVisitorInfoDetailsService
    {
        Task<int> SaveVisitorEntryDetails(VisitorEntryDetails visitorInfo);
        Task<IEnumerable<VisitorEntryDetails>> GetAllVisitorEntryDetails();
        Task<VisitorEntryDetails> GetVisitorEntryDetailsById(int id);
        Task<bool> DeleteVisitorEntryDetailsById(int id);

        Task<string> GetCardNo();
    }
}
