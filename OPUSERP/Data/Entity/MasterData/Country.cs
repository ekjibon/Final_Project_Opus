using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Data.Entity.Master
{
    public class Country : Base
    {
        [Required]
        public string countryCode { get; set; }

        [Required]
        public string countryName { get; set; }
        public string countryNameBn { get; set; }

        
        public string shortName { get; set; }

    }
}
