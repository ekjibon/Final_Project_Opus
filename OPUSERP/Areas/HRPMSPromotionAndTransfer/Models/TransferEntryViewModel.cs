using OPUSERP.Areas.HRPMSPromotionAndTransfer.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSPromotionAndTransfer.Models
{
    public class TransferEntryViewModel
    {
        public string employeeId { get; set; }

        public int transfarId { get; set; }

        [Required]
        public int id { get; set; }

        public string employeeName { get; set; }

        public string designation { get; set; }

        [Required]
        [Display(Name = "Curent Designation")]
        public string currentDesignation { get; set; }

        public string currentPlace { get; set; }

        public string currentGrade { get; set; }

        [Required]
        [Display(Name = "New Designation")]
        public string newDesignation { get; set; }

        public string newPlace { get; set; }

        public string newGrade { get; set; }

        public string orderDate { get; set; }

        public string effectDate { get; set; }

        public string joiningDate { get; set; }

        public string remark { get; set; }

        public PromotionTransferLn fLang { get; set; }

        public IEnumerable<TransferEntry> transferEntries { get; set; }

        public TransferEntry transferEntrie { get; set; }
    }
}
