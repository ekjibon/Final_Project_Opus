using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMPurchaseProcess.Models
{
    public class CSRequisitionList
    {
        
        public int?[] csEId { get; set; }
        public decimal?[] csEqty { get; set; }
        public decimal?[] csErate { get; set; }

        public IEnumerable<RequisitionForCSViewModel> requisitionForCSViewModels { get; set; }
        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
        public IEnumerable<CSMaster> cSMasters { get; set; }
        public CSMaster cSMaster { get; set; }
        public ApproverPanelViewModel approverPanel { get; set; }
        public IEnumerable<ApproverPanelViewModel> approerPanelList { get; set; }
        public IEnumerable<QuotationDetailsVM> quotationDetailsVMs { get; set; }
        public IEnumerable<CSDetailsVM> cSDetailsVMs { get; set; }
        public IEnumerable<Justification> justifications { get; set; }
    }
}
