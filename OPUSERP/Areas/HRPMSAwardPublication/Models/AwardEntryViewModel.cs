using OPUSERP.Areas.HRPMSAwardPublication.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAwardPublication.Models
{
    public class AwardEntryViewModel
    {
    
        public int employeeId { get; set; }

        public int awardId { get; set; }
       
        public string employeeName { get; set; }
        
        public string designation { get; set; }

        [Required]
        public string awardName { get; set; }

        [Required]
        public string purpose { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime awardDate { get; set; }

        public AwardLn flang { get; set; }

        public IEnumerable<AwardEntry> awards { get; set; }

        public AwardEntry award { get; set; }
    }
}
