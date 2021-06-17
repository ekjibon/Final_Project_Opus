using OPUSERP.Areas.HRPMSTraining.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTraining.Models
{
    public class TrainingNameViewModel
    {
        public string name { get; set; }

        public string isMandatory { get; set; }

        public string type { get; set; }

        public string objective { get; set; }

        public string description { get; set; }


        public TrainingNameLn fLang { get; set; }
    }
}
