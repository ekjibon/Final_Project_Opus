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
    [Table("ProposedRating", Schema = "CRO")]
    public class ProposedRating : Base
    {
        public int operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        [MaxLength(150)]
        public string proposedIRCRatingTypeName { get; set; }
        [MaxLength(150)]
        public string proposedIRCRatingValue { get; set; }
        [MaxLength(150)]
        public string proposedIRCShortRatingName { get; set; }
        [MaxLength(150)]
        public string proposedIRCOutlook { get; set; }
        [MaxLength(150)]
        public string proposedEntityRatingName { get; set; }
    }
}
