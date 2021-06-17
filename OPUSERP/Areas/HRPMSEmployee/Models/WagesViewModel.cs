using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class WagesViewModel
    {
        public int editId { get; set; }

        public int employeeId { get; set; }

        public string reason { get; set; }

        public string remarks { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public IEnumerable<WagesLeftDetails> wagesLeftDetails { get; set; }
    }
}
