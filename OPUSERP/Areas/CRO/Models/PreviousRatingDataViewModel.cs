using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class PreviousRatingDataViewModel
    {

        public int Id { get; set; }   
        public int AgreementId { get; set; }   
             
        public string agreementNo { get; set; }     
        public string leadName { get; set; }     
        public string ratingYearName { get; set; }       
        public string ratingTypeName { get; set; }        
       
       
    }
}
