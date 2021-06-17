using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class ItemDisPriceFixingViewModelspec
    {
        public int? Id { get; set; }
        public int? itemSpecificationId { get; set; }
        public string itemSpecificationName { get; set; }

        public decimal? price { get; set; }

        public decimal? discountPersent { get; set; }
       

        public decimal? VAT { get; set; }
        public int? quantity { get; set; }
        public string unitName { get; set; }
        public string barcode { get; set; }
        public string itemName { get; set; }
        public string packageName { get; set; }
        public int? packageDetailId { get; set; }

        public int? isActive { get; set; }  // 0 = In Active, 1 = Active
    }
}
