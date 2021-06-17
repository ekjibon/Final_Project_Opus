using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Travel
{
    [Table("TravelMaster", Schema = "HR")]
    public class TravelMaster:Base
    {
        public string travelNumber { get; set; }

        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; } //RequisitionBy

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public string accountNumber { get; set; }

        public int? travelProjectId { get; set; }
        public HRProject travelProject { get; set; }

        public int? hRActivityId { get; set; }
        public HRActivity hRActivity { get; set; }

        public int? hRDonerId { get; set; }
        public HRDoner hRDoner { get; set; }

        public string purpose { get; set; }

        public int? status { get; set; } //1=Create ,2=onGoing, 3=finalApprove , 4=Return ,5=Reject

    }
}
