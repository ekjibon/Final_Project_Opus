using OPUSERP.Areas.HRPMSTrainingNew.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSTrainingNew.Models
{
    public class TrainingPlanningViewModel
    {
        public int? planningId { get; set; }

        public string course { get; set; }

        public string courseObjective { get; set; }

        public string amount { get; set; }

        public DateTime? planeStartDate { get; set; }

        public DateTime? planeEndDate { get; set; }

        public string year { get; set; }

        public int participant { get; set; }

        public int trainingType { get; set; }

        public int? employeeType { get; set; }
        public int[] employeeTypeMultiple { get; set; }

        public string budget { get; set; }

        public string remark { get; set; }

        public int? country { get; set; }

        public string location { get; set; }


        public TrainingPlanningLn fLang { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
        public IEnumerable<TrainingInfoNew> trainingInfoNews { get; set; }
        public IEnumerable<CourseTitle> courseTitles { get; set; }
        public IEnumerable<Year> years { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
