using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class PackagingViewModel
    {
        public DateTime? packageDate { get; set; }
        public string packageNo { get; set; }
        public int? packagemasterId { get; set; }
        public string packageName { get; set; }
        public string description { get; set; }
        public int? distributorTypeId { get; set; }
        public int?[] itemSpecificationIdall { get; set; }
      

        public int?[] quantityall { get; set; }

        public decimal?[] discountPersentall { get; set; }
       
      
        public string[] isfreeall { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public IEnumerable<DistributorType> distributorTypes { get; set; }
        public IEnumerable<PackageMaster> packageMasters { get; set; }
        public PackageMaster packageMaster { get; set; }
        public IEnumerable<PackageDetail> packageDetails { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public Company company { get; set; }

    }
}
