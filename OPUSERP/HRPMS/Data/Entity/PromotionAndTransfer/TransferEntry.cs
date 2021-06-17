using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer
{
    [Table("TransferEntry", Schema = "HR")]
    public class TransferEntry : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        //public string designation { get; set; }

        public string currentDesignation { get; set; }

        public string currentPlace { get; set; }

        public string currentGrade { get; set; }

        public string newDesignation { get; set; }

        public string newPlace { get; set; }

        public string newGrade { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string orderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string effectDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string joiningDate { get; set; }

        public string remark { get; set; }

        //approver
        public string status { get; set; }
    }
}
