using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class CourseTitleViewModel
    {
        public string courseTitleId { get; set; }
        [Required]
        public string courseTitle { get; set; }

        public CourseTitleLn fLang { get; set; }

        public IEnumerable<CourseTitle> courseTitles { get; set; }
    }
}
