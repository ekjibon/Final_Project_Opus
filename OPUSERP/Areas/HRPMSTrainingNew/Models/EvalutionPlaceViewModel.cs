using OPUSERP.Areas.HRPMSTrainingNew.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTrainingNew.Models
{
    public class EvalutionPlaceViewModel
    {
        public int? planningId { get; set; }

        public string[] completionStatus { get; set; }

        public int[] place { get; set; }

        public string[] remark { get; set; }
         
        public int[] placeId { get; set; }

        public TrainingPlanningLn fLang { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
        public IEnumerable<EnrolledTrainee> enrolledTrainees { get; set; }
        public TrainingInfoNew trainingInfoNew { get; set; }
    }
}
