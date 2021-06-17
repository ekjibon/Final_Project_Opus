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
    [Table("IRCRating", Schema = "CRO")]
    public class IRCRating : Base
    {
        public int operationMasterId { get; set; }
        public OperationMaster operationMaster { get; set; }

        public DateTime? ircDate { get; set; }
        [MaxLength(150)]
        public string ratingTypeName { get; set; }
        [MaxLength(150)]
        public string ratingValue { get; set; }
        [MaxLength(150)]
        public string shortRatingName { get; set; }
        [MaxLength(150)]
        public string outlook { get; set; }
        [MaxLength(150)]
        public string entityRatingName { get; set; }
    }
}
