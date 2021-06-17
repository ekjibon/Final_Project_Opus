using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("MenuMealTypeMapping", Schema = "OSales")]
    public class MenuMealTypeMapping : Base
    {
        public int? salesMenuId { get; set; }
        public SalesMenu salesMenu { get; set; }

        public int? mealTypeId { get; set; }
        public MealType mealType { get; set; }        
        
        [MaxLength(10)]
        public string activeStatus { get; set; }
    }
}
