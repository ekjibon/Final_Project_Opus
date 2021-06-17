using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Rental.Data.Entity.Sales
{
   
    [Table("RentTermsConditions", Schema = "Rental")]
    public class RentTermsConditions : Base
    {
       

        public int? salesInvoiceMasterId { get; set; }
        public RentInvoiceMaster salesInvoiceMaster { get; set; }
       public string termtext { get; set; }

    }
}
