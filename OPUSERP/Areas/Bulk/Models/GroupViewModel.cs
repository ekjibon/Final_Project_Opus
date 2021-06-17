using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Bulk.Models
{
    public class GroupViewModel
    {
        public int groupId { get; set; }
        public string name { get; set; }
        public int[] ids { get; set; }

        public string subject { get; set; }
        public string description { get; set; }

        public string SMS { get; set; }
        public string Email { get; set; }

        public IEnumerable<MemberGroup> memberGroups { get; set; }

        public RlnMemberGroup rlnMemberGroup { get; set; }

        public IEnumerable<RlnMemberGroup> rlnMemberGroups { get; set; }

        public IEnumerable<OPUSERP.CLUB.Data.Member.MemberInfo> employeeInfos { get; set; }
    }
}
