using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class RatingYearViewModel
    {
        public int ratingYearId { get; set; }

        [Required]
        public string ratingYear { get; set; }

        public IEnumerable<RatingYear> ratingYears { get; set; }
    }
}
