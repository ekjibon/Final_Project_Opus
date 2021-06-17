using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("OperationMaster", Schema = "CRO")]
    public class OperationMaster : Base
    {
        public int? agreementId { get; set; }
        public Agreement  agreement { get; set; }
        public int? approvedforCroId { get; set; }
        public ApprovedforCro approvedforCro { get; set; }

        [MaxLength(150)]
        public string assignTeam { get; set; }
        [MaxLength(150)]
        public string assignTeamLeader { get; set; }
        public DateTime? assignTeamDate { get; set; }
        public DateTime? assignCoHeadDate { get; set; }
        [MaxLength(150)]
        public string assignCoHead { get; set; }
        public DateTime? assignDate { get; set; }
        [MaxLength(150)]
        public string assignTo { get; set; }
        public DateTime? tLDeclarationDate { get; set; }
        public int? declaration { get; set; }

        public int? jobStatusId { get; set; }
        public JobStatus jobStatus { get; set; }
        [MaxLength(100)]
        public string reportNo { get; set; }
        public DateTime? reportDate { get; set; }
        [MaxLength(550)]
        public string comments { get; set; }
        public DateTime? assignDateReview { get; set; }
        public DateTime? assignDateConversion { get; set; }
        [MaxLength(150)]
        public string assignToReviewer { get; set; }
        [MaxLength(150)]
        public string assignToConverter { get; set; }
        
    }
}
