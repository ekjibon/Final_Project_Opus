using Microsoft.AspNetCore.Http;
using OPUSERP.SEBA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SEBA.Models
{
    public class ProblemMasterViewModel
    {
        public int problemMasterId { get; set; }
        public string tokenNo { get; set; }       
        public string priority { get; set; }
        public DateTime? targetDate { get; set; }

        public int?[] headIdAll { get; set; }
        public string[] problemTitleAll { get; set; }
        public string[] descriptionAll { get; set; }

        public IFormFile imagePath_Challan { get; set; }

        //public IEnumerable<CustomerProductWarranty> customerProductWarranties { get; set; }
        public IEnumerable<ProblemMaster> problemMasters { get; set; }
    }
}
