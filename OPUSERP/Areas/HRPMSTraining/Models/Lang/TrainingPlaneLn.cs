using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTraining.Models.Lang
{
    public class TrainingPlaneLn
    {
        public string title { get; set; }
        public string planeName { get; set; }
        public string planeStartDate { get; set; }
        public string planeEndDate { get; set; }
        public string trainingName { get; set; }
        public string duration { get; set; }
        public string participant { get; set; }
        public string trainingType { get; set; }
        public string noOfTraining { get; set; }
        public string expectedStartDate { get; set; }
        public string expectedEndDate { get; set; }
        public string organization { get; set; }
        public string action { get; set; }
    }
}
