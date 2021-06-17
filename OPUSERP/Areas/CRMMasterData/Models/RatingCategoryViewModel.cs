using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class RatingCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ratingCategoryName { get; set; }

        public IEnumerable<RatingCategory> ratingCategories { get; set; }
    }
}
