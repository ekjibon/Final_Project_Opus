using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class BranchDetailsViewModel
    {
        public int? branchDetailsId { get; set; }
        public int bankId { get; set; }
        public int branchId { get; set; }
        public int? thanaId { get; set; }
        public string accountNo { get; set; }
        public string mailingAddress { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string collectionType { get; set; }
        public Bank Bank { get; set; }
        public BankBranch BankBranch { get; set; }
        public Thana Thana { get; set; }

        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<BankBranch> bankBranches { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<BankBranchDetails> bankBranchDetails { get; set; }
    }
}
