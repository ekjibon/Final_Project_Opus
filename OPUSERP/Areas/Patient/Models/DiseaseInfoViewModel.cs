using OPUSERP.Patient.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Models
{
    public class DiseaseInfoViewModel
    {
        public int diseaseId { get; set; }
        public string diseaseName { get; set; }        
        public string diseaseDescription { get; set; }

        public IEnumerable<DiseaseInfo> diseaseInfos { get; set; }
    }
}
