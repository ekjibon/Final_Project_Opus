using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("ExamResult", Schema = "HR")]
    public class ExamResult:Base
    {
        public int? trainingExamId { get; set; }
        public TrainingExam trainingExam { get; set; }

        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string optainedMarks { get; set; } 

        public string comment { get; set; }
    }
}
