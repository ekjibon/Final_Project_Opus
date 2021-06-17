using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Data.Entity
{
    [Table("CandidateInfo", Schema = "RCRT")]
    public class CandidateInfo : Base
    {
        public int? jobReqId { get; set; }
        public JobRequisition jobReq { get; set; }

        [MaxLength(30)]
        public string candidateCode { get; set; }        
       
        public string nationalID { get; set; }
        public string candidateName { get; set; }
        public string nationality { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }
        public string gender { get; set; }
        public string placeOfBirth { get; set; }
        public string maritalStatus { get; set; }
        public string religion { get; set; }
        public int? department { get; set; }
        public string bloodGroup { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string homeDistrict { get; set; }
        public string designation { get; set; }
        public int? status { get; set; }
       

      //  public ApplicationUser ApplicationUser { get; set; }

    }
}
