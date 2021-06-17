using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("MealType", Schema = "OSales")]
    public class MealType : Base
    {
        [MaxLength(300)]
        public string mealTypeName { get; set; }       
        [MaxLength(10)]
        public string activeStatus { get; set; }
    }
}
