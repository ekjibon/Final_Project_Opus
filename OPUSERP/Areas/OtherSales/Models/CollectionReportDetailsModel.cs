using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class CollectionReportDetailsModel
    {
        public string invoiceNumber { get; set; }
        public string collectionNumber { get; set; }
        public DateTime? collectionDate { get; set; }
        public decimal? collectionAmount { get; set; }

       
       
        
    }
}
