using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models.NotMappedEntity
{
    public class MemberwithPhoto
    {
        public OPUSERP.CLUB.Data.Member.MemberInfo employeeInfo { get; set; }

        public string photoUrl { get; set; }
    }
}
