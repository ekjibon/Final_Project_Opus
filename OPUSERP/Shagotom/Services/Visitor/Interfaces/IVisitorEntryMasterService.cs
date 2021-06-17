using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Shagotom.Data.Entity.Visitor;

namespace OPUSERP.Shagotom.Services.Visitor.Interfaces
{
    public interface IVisitorEntryMasterService
    {
        Task<int> SaveVisitorEntryMaster(VisitorEntryMaster visitorEntry);
        Task<IEnumerable<VisitorEntryMaster>> GetAllVisitorEntryMaster();
        Task<VisitorEntryMaster> GetVisitorEntryMasterById(int id);
        Task<bool> DeleteVisitorEntryMasterById(int id);
    }
}
