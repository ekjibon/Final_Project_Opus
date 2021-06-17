using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Training
{
    [Table("TrainingExam", Schema = "HR")]
    public class TrainingExam:Base
    {
        public string name { get; set; }

        public int? trainingInfoId { get; set; }
        public TrainingInfo trainingInfo { get; set; }

        public int? trainerId { get; set; }
        public Trainer trainer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startTimestamp { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? endTimestamp { get; set; }

        public string modeOfExam { get; set; } // Viva, Written, MCQ

        public string syllabus { get; set; }

        public string maxValue { get; set; }

        public string minValue { get; set; }

        public string unit { get; set; }
    }
}
