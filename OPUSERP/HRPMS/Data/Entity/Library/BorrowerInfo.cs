using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Library
{
    [Table("BorrowerInfo", Schema = "HR")]
    public class BorrowerInfo: Base
    {
        //Foreign Reliation
        public int borrowerId { get; set; }
        public EmployeeInfo borrower { get; set; }

        public int? bookEntryId { get; set; }
        public BookEntry bookEntry { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBorrow { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfReturn { get; set; }

        public string noOfCopy { get; set; }

        public int isReturned { get; set; }
    }
}
