using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class CroDocumentsViewModel
    {
        public int? Id { get; set; }
        public int? operationMasterId { get; set; }
        public DateTime? receiveDate { get; set; }
  
        public string createdBy { get; set; }
        public string description { get; set; }
        public string documentdescription { get; set; }
        public string documentName { get; set; }
       
        

    }
}
