using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
    public interface ITraningHistoryService
    {
        Task<bool> SaveTraningHistory(MemberTraningLog traningLog);
        Task<IEnumerable<MemberTraningLog>> GetTraningHistory();
        Task<MemberTraningLog> GetTraningHistoryById(int id);
        Task<bool> DeleteTraningHistoryById(int id);
        Task<IEnumerable<MemberTraningLog>> GetTraningHistoryByEmpId(int empId);

        //Wahid
        Task<IEnumerable<MemberTraningLog>> GetTraningHistoryListById(int? empId);
        //Wahid
    }
}
