using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class TransferPostingViewModel
    {
        [Required]
        [Display(Name = "Work Station")]
        public string workStation { get; set; }
        [Required]
        [Display(Name = "From date")]
        public string trFromDate { get; set; }
        [Required]
        [Display(Name = "To date")]
        public string trToDate { get; set; }
        [Required]
        [Display(Name = "Duration (Year)")]
        public string duration { get; set; }
        [Display(Name = "Grade")]
        public string grade { get; set; }
        [Display(Name = "Designation")]
        public string designation { get; set; }
        public string action { get; set; }

        public TransferPosting fLang  { get; set;}
    }
}
