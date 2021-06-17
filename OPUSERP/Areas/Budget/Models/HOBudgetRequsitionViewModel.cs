﻿using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class HOBudgetRequsitionViewModel
    {
        public string Number { get; set; }
        public int? reqId { get; set; }
        public int? year { get; set; }
        public DateTime? Date { get; set; }

        public int?[] heads { get; set; }
        public decimal?[] amounts { get; set; }

        public string[] dbField { get; set; }
        public string[] headName { get; set; }
        public decimal[] col1 { get; set; }
        public decimal[] col2 { get; set; }
        public decimal[] col3 { get; set; }
        public decimal[] col4 { get; set; }
        public decimal[] col5 { get; set; }
        public decimal[] col6 { get; set; }
        public decimal[] col7 { get; set; }
        public decimal[] col8 { get; set; }
        public decimal[] col9 { get; set; }
        public decimal[] col10 { get; set; }
        public decimal[] col11 { get; set; }
        public decimal[] col12 { get; set; }

        public BudgetRequisitionLn flang { get; set; }
        public BudgetRequisitionExcelLn flang1 { get; set; }
        public IEnumerable<HOBudgetRequsitionMaster> hOBudgetRequsitionMasters { get; set; }
        public IEnumerable<HOBudgetRequsitionDetail> hOBudgetRequsitionDetails { get; set; }
        public IEnumerable<HOBudgetRequsitionDetail> budgetRequsitionDetails { get; set; }
        public IEnumerable<FiscalYear> fiscalYears { get; set; }
        public FiscalYear fiscalYear { get; set; }
        public IEnumerable<ApproverPanelViewModel> approerPanelList { get; set; }
        public List<ApprovalPanel> panels { get; set; }
        public IEnumerable<BudgetRequisitionApprovedListViewModel> budgetRequisitionApprovedListViewModels { get; set; }
        public IEnumerable<BudgetRequsitionMaster> budgetRequsitionMasters { get; set; }
    }
}
