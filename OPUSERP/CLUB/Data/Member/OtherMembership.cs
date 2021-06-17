using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Member
{
    [Table("OtherMembership", Schema = "Club")]
    public class OtherMembership : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public MemberInfo employee { get; set; }

        public string nameOrganization { get; set; }

        public string membershipType { get; set; }

        public int? membershipId { get; set; }
        public Membership membership { get; set; }

        public string remarks { get; set; }
    }
}
