using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRO.Data.Entity.DistributeJob
{
    [Table("RatingValue", Schema = "CRO")]
    public class RatingValue : Base
    {
        public int? ratingTypeId { get; set; }
        public RatingType ratingType { get; set; }
        public int? ratingCategoryId { get; set; }
        public RatingCategory ratingCategory { get; set; }

        [MaxLength(350)]
        public string ratingValues { get; set; }
    }
}
