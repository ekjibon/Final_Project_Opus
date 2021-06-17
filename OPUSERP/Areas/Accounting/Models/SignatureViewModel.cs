using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity.AttachmentComment;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class SignatureViewModel
    {
       
        public int signatureId { get; set; }
        public int? signatureTypeId { get; set; }
        public int? employeeInfoId { get; set; }
        public int? signatureSlNo { get; set; }
        public string active { get; set; }

        public IEnumerable<SignatureType> signatureTypes { get; set; }
        public IEnumerable<Signature> signatures { get; set; }
        
       
        
    }
}
