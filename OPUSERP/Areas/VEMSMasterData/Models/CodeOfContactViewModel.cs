using OPUSERP.VEMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSMasterData.Models
{
    public class CodeOfContactViewModel
    {
        public int? cocId { get; set; }

        public string description { get; set; }


        public IEnumerable<CodeOfContact> codeOfContacts { get; set; }
        public CodeOfContact codeOfContact { get; set; }

    }
}
