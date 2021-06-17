using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.TrainingNew
{
    [Table("TrainingInfoNew", Schema = "HR")]
    public class TrainingInfoNew : Base
    {
        public string year { get; set; }
        public string course { get; set; }
        public string budget { get; set; }//DropDown Goverment & Development Partner
        public int noOfParticipants { get; set; }
        public int noOfParticipantsActual { get; set; }

        public int? employeeTypeId { get; set; }
        public EmployeeType employeeType { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? trainingCategoryId { get; set; }
        public TrainingCategory trainingCategory { get; set; }

        public int? trainingInstituteId { get; set; }
        public TrainingInstitute trainingInstitute { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDateActual { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endDateActual { get; set; }

        public string amount { get; set; }

        public string amountActual { get; set; }

        public string location { get; set; } //For Foreign Training

        public string courseObjective { get; set; }

        public string remarks { get; set; }

        public int status { get; set; }

        public int trainingType { get; set; } //1 For local, 2 for Foreign

        public string orgType { get; set; }

        //Traing People Mutiple

        public string employeeTypes { get; set; }
        public string employeeTypeNames { get; set; }
    }
}
