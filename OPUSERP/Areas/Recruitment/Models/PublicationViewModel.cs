using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class PublicationViewModel
    {
        public string candidateId { get; set; }

        public string publicationId { get; set; }

        [Required]
        [Display(Name = "Publication Name")]
        public string publicationName { get; set; }

        [Required]
        [Display(Name = "Publication Type")]
        public string publicationType { get; set; }

        [Display(Name = "Publication Place")]
        public string publicationPlace { get; set; }

        [Required]
        [Display(Name = "Publication Date")]
        public DateTime? publicationDate { get; set; }

        public string action { get; set; }

        public IEnumerable<RcrtPublication> rcrtPublications { get; set; }
    }
}
