using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class CadreViewModel
    {
        public string cadreId { get; set; }
        [Required]
        public string cadreName { get; set; }
        public string cadreNameBn { get; set; }
        
        public string cadreShortName { get; set; }

        public CadreInfoLn fLang { get; set; }

        public IEnumerable<Cadre> cadres { get; set; } 
    }
}
