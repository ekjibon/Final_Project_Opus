using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Member
{
    [NotMapped]
    public class MemberAvailablePosts
    {
        public int postId { get; set; }
        public int? orgId { get; set; }
        public string designationName { get; set; }
    }
}
