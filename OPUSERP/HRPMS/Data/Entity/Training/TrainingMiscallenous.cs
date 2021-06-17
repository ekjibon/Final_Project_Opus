using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingMiscallenous", Schema = "HR")]
    public class TrainingMiscallenous : Base
    {
        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        public string activityHead { get; set; }

        public string activityDetails { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startTimestamp { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endTimestamp { get; set; }

        public string costOfAction { get; set; }
    }
}
