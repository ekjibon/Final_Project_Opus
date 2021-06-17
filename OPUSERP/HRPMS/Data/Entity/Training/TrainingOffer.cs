using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingOffer", Schema = "HR")]
    public class TrainingOffer : Base
    {
        public string name { get; set; }

        public string content { get; set; }

        public string syllabus { get; set; }

        public string benifits { get; set; }

        public string duration { get; set; }

        public string units { get; set; }

        public int? moduleTrainingCategoryId { get; set; }
        public TrainingCategory moduleTrainingCategory { get; set; }

        public string type { get; set; }
    }
}
