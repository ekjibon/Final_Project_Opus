using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.Accounting.Models
{
    [NotMapped]
    public class GetSignatureViewModel
    {
        public string FirstSignatureName { get; set; }
        public string FirstSignatureDesignation { get; set; }
        public string SecondSignatureName { get; set; }
        public string SecondSignatureDesignation { get; set; }
        public string ThirdSignatureName { get; set; }
        public string ThirdSignatureDesignation { get; set; }
        public string FourthSignatureName { get; set; }
        public string FourthSignatureDesignation { get; set; }

    }
}
