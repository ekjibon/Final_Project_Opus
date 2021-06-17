using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("RcrtPublication", Schema = "RCRT")]
    public class RcrtPublication : Base
    {
        public int candidateId { get; set; }
        public CandidateInfo candidate { get; set; }

        public string publicationName { get; set; }

        public string publicationType { get; set; }

        public string publicationPlace { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? publicationDate { get; set; }
    }
}
