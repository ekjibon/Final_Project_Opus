using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Member
{
    [Table("MemberChildren", Schema = "Club")]
    public class MemberChildren : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public MemberInfo employee { get; set; }

        public string childName { get; set; }

        public string childNameBN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }
        
        public string education { get; set; }
        
        public string gender { get; set; }
        
        public string bin { get; set; }

        public string occupation { get; set; }
        
        public string designation { get; set; }
        
        public string organization { get; set; }

        [MaxLength(100)]
        public string nid { get; set; }

        public string bloodGroup { get; set; }

        public int disability { get; set; }

        //SpecialSkill
        public string spacialSkillIds { get; set; }
        public string spacialSkills { get; set; }
    }
}
