using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class YearViewModel
    {
        public string yearId { get; set; }
        [Required]
        public string yearTitle { get; set; }

        public YearLn fLang { get; set; }

        public IEnumerable<Year> years { get; set; }
    }
}
