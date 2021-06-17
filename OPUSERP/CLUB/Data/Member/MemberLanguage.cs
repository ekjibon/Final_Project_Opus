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
    [Table("MemberLanguage", Schema = "Club")]
    public class MemberLanguage : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public MemberInfo employee { get; set; }

        [MaxLength(50)]
        public string reading { get; set; }

        [MaxLength(50)]
        public string writing { get; set; }

        [MaxLength(50)]
        public string speaking { get; set; }

        public int? languageId { get; set; }
        public Language language { get; set; }

        [MaxLength(100)]
        public string proficiency { get; set; }
    }
}
