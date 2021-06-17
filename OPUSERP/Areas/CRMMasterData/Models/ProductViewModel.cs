using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ProductViewModel
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }

        public IEnumerable<Product> products { get; set; }
    }
}
