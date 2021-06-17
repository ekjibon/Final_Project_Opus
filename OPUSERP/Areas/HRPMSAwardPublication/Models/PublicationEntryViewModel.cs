using OPUSERP.Areas.HRPMSAwardPublication.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSAwardPublication.Models
{
    public class PublicationEntryViewModel
    {
        [Required]
        public int employeeId { get; set; }

        public int publicationId { get; set; }
        
        public string employeeName { get; set; }
      
        public string designation { get; set; }
       
        public string publicationType { get; set; }
    
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string nameOfPublication { get; set; }

        public string place { get; set; }

        public PublicationLn fLang { get; set; }

        public IEnumerable<PublicationEntry> publicationEntries { get; set; }

        public PublicationEntry publicationEntrie { get; set; }
    }
}
