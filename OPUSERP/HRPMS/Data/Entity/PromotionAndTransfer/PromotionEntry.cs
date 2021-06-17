using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer
{
    [Table("PromotionEntry", Schema = "HR")]
    public class PromotionEntry : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        //public string designation { get; set; }//

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string date { get; set; }

        public string payScale { get; set; }

        public string nature { get; set; }

        public string basic { get; set; }

        public string goNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string goDate { get; set; }

        public string remark { get; set; }

        //approver
        public string status { get; set; }
    }
}
