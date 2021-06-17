using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class SubjectViewModel
    {
        public int subjectId { get; set; }
        [Required]
        public string subjectName { get; set; }
        public string subjectNameBn { get; set; }
        
        public string subjectShortName { get; set; }   
        
        public SubjectInfoLn fLang { get; set; }

        public IEnumerable<Subject> subjects { get; set; }
    }
}
