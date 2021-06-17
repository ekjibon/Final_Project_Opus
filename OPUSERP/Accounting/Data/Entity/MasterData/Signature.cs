using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.MasterData
{
    [Table("Signature", Schema = "ACCOUNT")]
    public class Signature : Base
    {
        public int? signatureTypeId { get; set; }
        public SignatureType signatureType { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo  employeeInfo { get; set; }

        public int? signatureSlNo { get; set; }

        public string active { get; set; }


    }
}
