using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class AutoVoucherMasterViewModel
    {
        public int masterId { get; set; }
        public int? autoVoucherNameId { get; set; }
        public int? voucherTypeId { get; set; }
        public int? projectId { get; set; }
        public string description { get; set; }

        public int?[] headIdAll { get; set; }
        public int?[] modeIdAll { get; set; }

        public int? transectionModeId { get; set; }

        public IEnumerable<AutoVoucherName> autoVoucherNames { get; set; }
        public IEnumerable<VoucherType> voucherTypes { get; set; }
        public IEnumerable<TransectionMode> transectionModes { get; set; }
        public IEnumerable<AutoVoucherMaster> autoVoucherMasters { get; set; }
    }
}
