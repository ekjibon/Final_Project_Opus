using OPUSERP.VEMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSMasterData.Models
{
    public class RequeredDocumentViewModel
    {
        public int? reqDocId { get; set; } 

        public string description { get; set; }


        
        public IEnumerable<RequiredDocuments> requiredDocuments { get; set; }
        public RequiredDocuments requiredDocument { get; set; }
    }
}
