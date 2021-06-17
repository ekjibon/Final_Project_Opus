using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingAllocation", Schema = "HR")]
    public class TrainingAllocation : Base
    {
        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        //Foreign Relation -> Resource
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? postDetailsId { get; set; }
        public PostDetails postDetails { get; set; }

        public string priorityLevel { get; set; }

        public string remarks { get; set; }
    }
}
