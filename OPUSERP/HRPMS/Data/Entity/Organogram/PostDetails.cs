using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Organogram
{
    [Table("PostDetails", Schema = "HR")]
    public class PostDetails : Base
    {
        public int? postId { get; set; }
        public Post post { get; set; }

        public int? reportingBossId { get; set; }
        public PostDetails reportingBoss { get; set; }

        //Foreign Reliation
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? postingTypeId { get; set; }
        public PostingType postingType { get; set; }

        public int? employmentTypeId { get; set; }
        public EmploymentType employmentType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? assaignDate { get; set; }

        public int? AssaignBy { get; set; }

        public string remarks { get; set; }

    }
}
