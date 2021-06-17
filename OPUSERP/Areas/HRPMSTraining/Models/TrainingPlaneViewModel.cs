using OPUSERP.Areas.HRPMSTraining.Models.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTraining.Models
{
    public class TrainingPlaneViewModel
    {
        public string planeName { get; set; }

        public string planeStartDate { get; set; }

        public string planeEndDate { get; set; }

        public string trainingName { get; set; }

        public string durationValue { get; set; }

        public string durationUnit { get; set; }

        public string participant { get; set; }

        public string trainingType { get; set; }

        public string noOfTraining { get; set; }

        public string expectedStartDate { get; set; }

        public string expectedEndDate { get; set; }

        public string organization { get; set; }


        public TrainingPlaneLn fLang { get; set; }
    }
}
