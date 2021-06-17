using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.CLUB.Data.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class MemberTypeViewModel
    {
        public int memberTypeId { get; set; }
        [Required]
        [Display(Name = "Member Type")]
        public string memberType { get; set; }
        public string memberTypeBn { get; set; }

        public EmployeeTypeLn fLang { get; set; }

        public IEnumerable<MemberType> memberTypes { get; set; }
    }
}
