using Microsoft.AspNetCore.Http;
using OPUSERP.Recruitment.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Recruitment.Models
{
    public class CandidatePhotographViewModel
    {
        public int candidateId { get; set; }

        public int photographId { get; set; }

        [Display(Name = "Candidate Photo")]
        public IFormFile candidatePhoto { get; set; }

        public CandidatePhoto photograph { get; set; }

        public string candidateNameCode { get; set; }

        public CandidateInfo candidateInfo { get; set; }
    }
}
