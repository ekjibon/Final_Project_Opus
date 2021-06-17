using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VMS.Models
{
    public class SORDetailsViewModel
    {
        public int sORMasterId { get; set; }

        public string sorNumber { get; set; }

        public string sorTitle { get; set; }

        public string duration { get; set; }

        public int numberOfItems { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }

        public string grandTotal { get; set; }

        public int[] sparePartsId { get; set; }

        public int[] vendorLength { get; set; }

        public int?[] supplierId { get; set; }

        public string[] rate { get; set; }
    }
}
