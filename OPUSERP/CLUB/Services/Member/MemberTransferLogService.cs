using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Member
{
    public class MemberTransferLogService : IMemberTransferLogService
    {
        private readonly ERPDbContext _context;

        public MemberTransferLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberTransferLog>> GetOrganization()
        {
            return await _context.memberTransferLogs.Include("employee").AsNoTracking().Distinct().ToListAsync();
        }

        public async Task<IEnumerable<MemberInfoReportViewModel>> OrganizationWiseMemberInfoReport(int memberTypeId, int org)
        {
            return await _context.memberInfoReportViewModels.FromSql($"[spRptOrganizationWiseMember] {memberTypeId},{org}").AsNoTracking().ToListAsync();
        }
    }
}
