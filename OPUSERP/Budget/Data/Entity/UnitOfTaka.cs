using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("UnitOfTaka", Schema = "Budget")]
    public class UnitOfTaka:Base
    {
        public string UnitName { get; set; }
        public decimal? value { get; set; }
        public int? status { get; set; }
    }
}
