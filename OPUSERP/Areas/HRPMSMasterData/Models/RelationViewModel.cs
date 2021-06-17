using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class RelationViewModel
    {
        public int relationId { get; set; }
        [Required]
        public string relationName { get; set; }
        public string relationNameBn { get; set; }
        
        public string relationShortName { get; set; }

        public EmployeeRelationLn fLang { get; set; }

        public IEnumerable<Relation> relations { get; set; }
    }
}
