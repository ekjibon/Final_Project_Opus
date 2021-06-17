using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Areas.Production.Models
{
    public class BOMViewModel
    {
        
        public int? bOMMasterId { get; set; }
        public string bomNo { get; set; }
        public int? bomitemSpecificationId { get; set; }
        public string bomItemDescription { get; set; }
        public decimal? bomQty { get; set; }
        public int? isactive { get; set; }

        public DateTime? bomDate { get; set; }
        public int[] itemId { get; set; }
        public string[] itemSpecification { get; set; }
        public decimal?[] quantity { get; set; }
        public decimal?[] rate { get; set; }
        public decimal?[] wastePercent { get; set; }

        public int?[] ledgerId { get; set; }
        public decimal?[] cost { get; set; }
        public string[] costType { get; set; }




        public IEnumerable<BOMDetail>   bOMDetails { get; set; }
        public IEnumerable<BOMMaster> bOMMasters { get; set; }

        public BOMMaster GetBOMMasterById { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<ProductionDocumentAttachment> photoes { get; set; }
        public IEnumerable<ProductionDocumentAttachment> documents { get; set; }
        public Company company { get; set; }

        
    }
}
