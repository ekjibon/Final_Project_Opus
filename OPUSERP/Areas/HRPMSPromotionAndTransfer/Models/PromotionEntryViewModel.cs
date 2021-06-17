using OPUSERP.Areas.HRPMSPromotionAndTransfer.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSPromotionAndTransfer.Models
{
    public class PromotionEntryViewModel
    {
        
        public int employeeId { get; set; }

        [Required]
        public int id { get; set; }


        public int promotionId { get; set; }

        public string employeeName { get; set; }

        public string designation { get; set; }

        [Required]
        [Display(Name = "Promotion Date")]
        public string date { get; set; }

        public string payScale { get; set; }

        public string nature { get; set; }

        public string basic { get; set; }

        public string goNumber { get; set; }

        public string goDate { get; set; }

        public string remark { get; set; }

        public PromotionTransferLn fLang { get; set; }

        public IEnumerable<PromotionEntry> promotionEntries { get; set; }

        public PromotionEntry promotionEntry { get; set; }
    }
}
