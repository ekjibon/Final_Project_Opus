using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Data.Entity.Master
{
    public class PostOffice : Base
    {
        public int districtId { get; set; }
        public District district { get; set; }

        [MaxLength(10)]
        public string postalCode { get; set; }
        [MaxLength(100)]
        public string postalName { get; set; }
        [MaxLength(20)]
        public string postalShortName { get; set; }
        [MaxLength(100)]
        public string postalNameBn { get; set; }

        
    }
}
