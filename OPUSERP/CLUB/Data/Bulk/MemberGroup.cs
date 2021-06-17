using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Bulk
{
    [Table("MemberGroup", Schema = "Club")]
    public class MemberGroup:Base
    {
        public string name { get; set; }
        public string remarks { get; set; }
    }
}
