using OPUSERP.SCM.Data.Entity.Disbarse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Disbarse.Interface
{
    public interface IDisbarseMasterService
    {
        Task<int> SaveDisbarseMasterAsync(DisbarseMaster disbarseMaster);
        Task<IEnumerable<DisbarseMaster>> GetDisbarseMasterAsync();
        Task<DisbarseMaster> GetDisbarseMasterByIdAsync(int id);
        Task<bool> DeleteDisbarseMasterByIdAsync(int id);

        Task<string> GetDisbarseIOUNo();
    }
}
