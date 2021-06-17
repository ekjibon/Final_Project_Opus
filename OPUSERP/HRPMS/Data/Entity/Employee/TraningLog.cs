using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("TraningLog", Schema = "HR")]
    public class TraningLog : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? toDate { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? trainingCategoryId { get; set; }
        public TrainingCategory trainingCategory { get; set; }

        public int? trainingInstituteId { get; set; }
        public TrainingInstitute trainingInstitute { get; set; }

        public string remarks { get; set; }

        public string trainingTitle { get; set; }

        public string sponsoringAgency { get; set; }

    }
}
