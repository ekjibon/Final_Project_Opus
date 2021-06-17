using OPUSERP.HRPMS.Data.Entity.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Visitor.Interface
{
   public interface IPIMSVisitorService
    {
        Task<bool> SavePIMSVisitor(PIMSVisitor pIMSVisitor);
        Task<IEnumerable<PIMSVisitor>> GetPIMSVisitor();
        Task<PIMSVisitor> GetPIMSVisitorById(int id);
        Task<bool> DeletePIMSVisitorById(int id);
    }
}
