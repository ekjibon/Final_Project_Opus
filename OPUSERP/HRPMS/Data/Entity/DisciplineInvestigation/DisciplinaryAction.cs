using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation
{
    [Table("DisciplinaryAction", Schema = "HR")]
    public class DisciplinaryAction : Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? OffenseId { get; set; }
        public Offense Offense { get; set; }

        public int? naturalPunishmentId { get; set; }
        public NaturalPunishment naturalPunishment { get; set; }
    
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime punishmentDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime startingDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime endDate { get; set; }

        public string goNumberWithDate { get; set; }

        public string goFileURL { get; set; }

        public string remarks { get; set; }

        //-> Approved or Pending |  final status -> initiate or locked
        [MaxLength(20)]
        public string status { get; set; }
    }
}
