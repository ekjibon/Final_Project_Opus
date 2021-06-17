using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingDeliverables", Schema = "HR")]
    //Training Gift
    public class TrainingDeliverables : Base
    {
        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        public string deliverable { get; set; }

        public string details { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dueDate { get; set; }
    }
}
