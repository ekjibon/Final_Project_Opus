using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("Evaluation", Schema = "HR")]
    public class Evaluation:Base
    {
        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        public int? trainerId { get; set; }
        public Trainer trainer { get; set; }

        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? evaluationFectorsId { get; set; }
        public EvaluationFectors evaluationFectors { get; set; }

        public string optainedValue { get; set; }

        public string comment { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? evaluationDate { get; set; }
    }
}
