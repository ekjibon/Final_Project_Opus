using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.MasterData
{
    [Table("BuyerItemMapping", Schema = "SCM")]
    public class BuyerItemMapping : Base
    {
        public int? itemCategoryId { get; set; }
        public ItemCategory itemCategory { get; set; }

        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }


    }
}
