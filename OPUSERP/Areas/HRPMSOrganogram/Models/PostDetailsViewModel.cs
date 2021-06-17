using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Models
{
    public class PostDetailsViewModel
    {
        public int postDetailsId { get; set; }

        public int postId { get; set; }

        public int reportingBossId { get; set; }

        public int employeeId { get; set; }

        public int postingTypeId { get; set; }

        public int employmentTypeId { get; set; }

        public string assaignDate { get; set; }

        public int? AssaignBy { get; set; }

        public string remarks { get; set; }

        public PostDetailsLn fLang { get; set; }

        public IEnumerable<Post> posts { get; set; }

        public IEnumerable<PostingType> postingTypes { get; set; }

        public IEnumerable<EmploymentType> employmentTypesposts { get; set; }

        public IEnumerable<PostDetails> postDetails { get; set; }
    }
}
