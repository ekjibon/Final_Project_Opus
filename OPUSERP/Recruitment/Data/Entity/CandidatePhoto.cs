using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("CandidatePhoto", Schema = "RCRT")]
    public class CandidatePhoto : Base
    {
        //Foreign Reliation
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        [Required]
        public string url { get; set; }

        public string remarks { get; set; }

        [Required]
        public string type { get; set; }
    }
}
