using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ItemSpecificationDepartmentModel
    {
        public int? itemId { get; set; }
        public int? itemSpecificationId { get; set; }
        public string SKUNumber { get; set; }
        public string specificationName { get; set; }
        public string specImageUrl { get; set; }
        public decimal? qty { get; set; }
        public int? storeDepartmentId { get; set; }
        public IEnumerable<ItemSpecificationDepartmentModel> itemSpecificationDepartmentModels { get; set; }
        public IEnumerable<MostRecentRequisitionViewModel> MostRecent { get; set; }
        public IEnumerable<TopTenRequisitionViewModel> TopTen { get; set; }
        public IEnumerable<FeaturedItemViewModel> Featured { get; set; }
        public IEnumerable<RequisitionDetailsHistoryModel> requisitionDetailsHistoryModels { get; set; }
        public IEnumerable<ApprovalLogHistoryLogModel> approvalLogHistoryLogModels { get; set; }

        public IEnumerable<SectionWiseDesignationModel> sectionWiseDesignations { get; set; }
        public IEnumerable<RequisitionDetail> requisitionDetails { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }
        public EmployeeBasicModel employeeBasic { get; set; }
        public LogUserPersonInformation logUsers { get; set; }
        public SalesInvoiceMaster salesInvoiceMaster   { get; set; }
    }
}
