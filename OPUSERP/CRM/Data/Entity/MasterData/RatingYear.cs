using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("RatingYear", Schema = "CRM")]
    public class RatingYear : Base
    {
        [MaxLength(100)]
        public string ratingYearName { get; set; }
        [MaxLength(100)]
        public string ratingType { get; set; }
        [MaxLength(100)]
        public string ratingTypeName { get; set; }
    }
}
