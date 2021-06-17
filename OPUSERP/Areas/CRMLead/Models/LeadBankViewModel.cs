using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadBankViewModel
    {
        public int Id { get; set; }
        public int leadsId { get; set; }
        public int? isPersonal { get; set; }
        public string leadName { get; set; }
        public int bankBranchDetailsId { get; set; }
        public string bankType { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string contactName { get; set; }
        public int? department { get; set; }
        public int? designation { get; set; }

        public Leads leads { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<BankBranch> bankBranches { get; set; }
        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }
        public IEnumerable<CRMDepartment> departments { get; set; }
        public IEnumerable<CRMDesignation> designations { get; set; }
        public IEnumerable<FIType> fITypes { get; set; }
    }
}
