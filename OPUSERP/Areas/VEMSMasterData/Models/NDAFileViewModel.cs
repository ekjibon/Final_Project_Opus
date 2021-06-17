using OPUSERP.VEMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSMasterData.Models
{
    public class NDAFileViewModel
    {
        public int? ndaId { get; set; }

        public string fileNmae { get; set; }

        public string filePath { get; set; }

        public string fileType { get; set; }


        
        public IEnumerable<NDAFile> nDAFiles { get; set; }
        public NDAFile nDAFile { get; set; }
    }
}
