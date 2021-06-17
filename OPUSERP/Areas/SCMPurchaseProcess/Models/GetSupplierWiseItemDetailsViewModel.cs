using Microsoft.AspNetCore.Http;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMPurchaseProcess.Models
{
    public class GetSupplierWiseItemDetailsViewModel
    {
        
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public string ItemCode  { get; set; }
        public string supplierName { get; set; }
        public int? supplierId { get; set; }
        public decimal? qty { get; set; }
        public decimal? rate { get; set; }
        public decimal? subTotal { get; set; }
        

        
    }
}
