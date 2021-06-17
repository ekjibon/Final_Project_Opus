using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member.Interfaces
{
   public interface IMemberTransferLogService
    {
        Task<IEnumerable<MemberTransferLog>> GetOrganization();
        Task<IEnumerable<MemberInfoReportViewModel>> OrganizationWiseMemberInfoReport(int memberTypeId, int org);
    }
}
