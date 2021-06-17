using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("RlnTrainingTrainer", Schema = "HR")]
    public class RlnTrainingTrainer : Base
    {
        public int? trainerId { get; set; }
        public Trainer trainer { get; set; }

        public int? trainingOfferId { get; set; }
        public TrainingOffer trainingOffer { get; set; }
    }
}
