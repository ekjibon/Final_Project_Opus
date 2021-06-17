using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Models
{
    public class PostingTypeViewModel
    {
        public int id { get; set; }
        public string nameEN { get; set; }
        public string nameBN { get; set; }
        public string remarks { get; set; }

        public PostingTypeLn fLang { get; set; }

        public IEnumerable<PostingType> postingTypes { get; set; }
    }
}
