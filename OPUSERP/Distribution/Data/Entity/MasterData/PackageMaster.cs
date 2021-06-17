using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("PackageMaster", Schema = "Distribution")]
    public class PackageMaster : Base
    {
        public DateTime? packageDate { get; set; }
        public string packageNo { get; set; }
        public string packageName { get; set; }
        public string description { get; set; }
        public int? distributorTypeId { get; set; }
        public DistributorType distributorType { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
