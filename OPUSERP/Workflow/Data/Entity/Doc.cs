using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Workflow.Data.Entity
{
    [Table("Doc", Schema = "WMS")]
    public class Doc:Base
    {
        public string number { get; set; }

        public string subject { get; set; }

        public string content { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int? isClose { get; set; }

        public int? isInitial { get; set; }

        public string noteType { get; set; }

        public int? docId { get; set; }
        public Doc doc { get; set; }
    }
}
