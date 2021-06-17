using OPUSERP.HRPMS.Data.Entity.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Library.Interfaces
{
    public interface IBorrowerInfoService
    {
        Task<int> SaveBorrowerInfo(BorrowerInfo borrowerInfo);
        Task<IEnumerable<BorrowerInfo>> GetBorrowerInfo();
        Task<BorrowerInfo> GetBorrowerInfoById(int id);
        Task<bool> DeleteBorrowerInfoById(int id);
        Task<IEnumerable<BorrowerInfo>> GetBorrowerInfoByBookId(int bookId);
        Task<IEnumerable<BorrowerInfo>> GetBorrowerInfoByEmpId(int empId);
    }
}
