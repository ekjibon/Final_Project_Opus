using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class LanguageViewModel
    {
        public int languageId { get; set; }
        [Required]
        public string languageName { get; set; }
        public string languageNameBn { get; set; }
        
        public string languageShortName { get; set; }

        public LanguageLn fLang { get; set; }

        public IEnumerable<Language> languages { get; set; }
    }
}
