using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("EvaluationFectors", Schema = "HR")]
    public class EvaluationFectors : Base
    {
        public string name { get; set; }

        public string details { get; set; }

        public string maxValue { get; set; }

        public string minValue { get; set; }

        public string unit { get; set; }

        public string status { get; set; } // Training <->Trainee
    }
}
