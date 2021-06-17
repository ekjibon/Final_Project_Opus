using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRO.Models
{
    public class CroStatusViewModel
    {
     
        public DateTime? createdAt { get; set; }
  
        public string createdBy { get; set; }
        public string remarks { get; set; }
        public string jobStatusName { get; set; }
      
       
        

    }
}
