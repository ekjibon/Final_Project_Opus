using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class IRCRatingViewModel
    {
        public int operationMasterId { get; set; }   
        public DateTime? ircDate { get; set; }       
        public string ratingTypeName { get; set; }     
        public string ratingValue { get; set; }       
        public string shortRatingName { get; set; }        
        public string outlook { get; set; }       
        public string entityRatingName { get; set; }
       
    }
}
