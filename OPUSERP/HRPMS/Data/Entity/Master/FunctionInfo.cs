using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Master
{
    [Table("FunctionInfo", Schema = "HR")]
    public class FunctionInfo: Base
    {
        public string branchUnitName { get; set; }

        public string branchUnitNameBN { get; set; }

        public string branchCode { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        public int? shortOrder { get; set; }
    }
}
