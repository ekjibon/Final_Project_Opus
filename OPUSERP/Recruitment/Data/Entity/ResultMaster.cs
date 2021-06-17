using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("ResultMaster", Schema = "RCRT")]
    public class ResultMaster : Base
    {
        public int candidateInfoId { get; set; }
        public CandidateInfo candidateInfo { get; set; }

        public int jobRequisitionId { get; set; }
        public JobRequisition jobRequisition { get; set; }

        public string examNumber { get; set; }
        public string remarks { get; set; }
    }
}
