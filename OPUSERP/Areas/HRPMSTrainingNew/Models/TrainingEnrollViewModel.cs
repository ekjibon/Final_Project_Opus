using OPUSERP.Areas.HRPMSTrainingNew.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSTrainingNew.Models
{
    public class TrainingEnrollViewModel
    {
        public int id { get; set; }

        public string course { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? planeStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? planeEndDate { get; set; }

        public string year { get; set; }

        public string participant { get; set; }

        public string employeeType { get; set; }

        public string budget { get; set; }

        public int noOfParticipantsActual { get; set; }

        public int employeeId { get; set; }

        public string organization { get; set; }

        public string designation { get; set; }

        public string employeeName { get; set; }

        public string nid { get; set; }

        public string address { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        public string resourcePersonId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDateActual { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDateActual { get; set; }


        public TrainingImplementationLn fLang { get; set; }

        public IEnumerable<EmployeeType> employeeTypes { get; set; }

        public IEnumerable<TrainingInfoNew> trainingInfoNews { get; set; }
        public TrainingInfoNew trainingInfoNew { get; set; }

        public IEnumerable<EnrolledTrainee> enrolledTrainees { get; set; }

        public IEnumerable<ResourcePerson> resourcePeople { get; set; }

        public IEnumerable<TrainingResourcePerson> trainingResourcePersons { get; set; }

    }
}
