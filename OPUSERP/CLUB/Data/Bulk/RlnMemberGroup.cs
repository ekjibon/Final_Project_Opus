using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Bulk
{
    [Table("RlnMemberGroup", Schema = "Club")]
    public class RlnMemberGroup:Base
    {
        public int? memberGroupId { get; set; }
        public MemberGroup memberGroup { get; set; }

        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }

        public string type { get; set; }
    }
}
