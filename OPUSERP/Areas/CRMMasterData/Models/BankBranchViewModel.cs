using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class BankBranchViewModel
    {
        public int bankId { get; set; }
        public int branchId { get; set; }
  
        public string bankName { get; set; }
        public string branchName { get; set; }

        public int? thanaId { get; set; }
        public Thana thana { get; set; }

        public int? fiTypeId { get; set; }
        public string fiTypeName { get; set; }

        public IEnumerable<Bank> banks { get; set; }
        public IEnumerable<BankBranch> bankBranches { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<FIType> fITypes { get; set; }
        
    }
}
