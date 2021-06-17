using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class PublicationViewModel
    {
        public string employeeID { get; set; }

        public string publicationId { get; set; }

        [Required]
        [Display(Name = "Publication Name")]
        public string publicationName { get; set; }

        [Display(Name = "Publication Type")]
        public string publicationType { get; set; }

        [Display(Name = "Publication Place")]
        public string publicationPlace { get; set; }

        [Display(Name = "Publication Date")]
        public DateTime? publicationDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public string publicationNubmer { get; set; }

        public Publication fLang { get; set; }

        public IEnumerable<MemberPublication> publications { get; set; }
    }
}
