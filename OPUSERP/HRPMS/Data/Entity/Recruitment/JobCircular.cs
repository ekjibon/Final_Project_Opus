using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;

namespace OPUSERP.HRPMS.Data.Entity.Recruitment
{
    [Table("JobCircular", Schema = "HR")]
    public class JobCircular : Base
    {
        public string reference { get; set; }

        public string recruitmentType { get; set; }

        public string positionName { get; set; }

        public string project { get; set; }

        public int noOfPosition { get; set; }

        //permanent provision project
        public string jobType { get; set; }

        [MaxLength(40)]
        public string location { get; set; }

        public string comments { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? applicationDeadLine { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? lastDateOfWrittenExam { get; set; }

        public string applicationFee { get; set; }

        public string educationalQualification { get; set; }

        public string mainSubject { get; set; }

        public string otherQualification { get; set; }

        public int leastExperience { get; set; }

        public int maxAge { get; set; }

        public int maxAgeQuota { get; set; }

        //approver
        public string status { get; set; }
    }
}
