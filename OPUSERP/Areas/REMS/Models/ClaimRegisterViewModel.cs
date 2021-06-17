using OPUSERP.REMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.REMS.Models
{
    public class ClaimRegisterViewModel
    {
        public int? claimAssaigneId { get; set; }
        public int? assetRegistrationId { get; set; }
        public int? warrentyId { get; set; }
        public int? AssignToId { get; set; }
        public int? claimId { get; set; }
        public int? assignToVendorId { get; set; }
        public int? isObsolete { get; set; }
        public DateTime? claimDate { get; set; }
        public DateTime? AssignDate { get; set; }
        public string claimDescription { get; set; }
        public string ProblemDescription { get; set; }
        public string AssignToName { get; set; }
        public string remarks { get; set; }
        public string deliveryMode { get; set; }
        public int? isWarrenty { get; set; }
        public string[] purposeList { get; set; }
        public decimal?[] amountList { get; set; }

        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }

        public IEnumerable<ClaimRegister> claimRegisters { get; set; }
        public IEnumerable<ClaimAssign> claimAssigns { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignViewModels { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignedViewModels { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignWarrentyViewModels { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignNonWarrentyViewModels { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignViewModelsNew { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimAssignViews { get; set; }
        public IEnumerable<ClaimAssignViewModel> claimReturnFromVendorViewModel { get; set; }
    }
}
