using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.TrainingNew
{
    [Table("EnrolledTrainee", Schema = "HR")]
    public class EnrolledTrainee : Base
    {
        //Foreign Reliation
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string name { get; set; }

        public string designation { get; set; }

        public string workingPlace { get; set; }

        public string address { get; set; }

        public string NID { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        public int? trainingInfoNewId { get; set; }
        public TrainingInfoNew trainingInfoNew { get; set; }

        public int positon { get; set; }

        public string completionStatus { get; set; } //Completed/NotCompleted

        public string result { get; set; }

        public string remarks { get; set; }

    }
}
