using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface IServiceHistoryService
    {
        Task<bool> SaveServiceHistory(MemberTransferLog traveInfo);
        Task<IEnumerable<MemberTransferLog>> GetServiceHistory();
        Task<MemberTransferLog> GetServiceHistoryById(int id);
        Task<bool> DeleteServiceHistoryById(int id);
        Task<IEnumerable<MemberTransferLog>> GetServiceHistoryByEmpId(int empId);
    }
}
