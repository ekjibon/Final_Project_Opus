using OPUSERP.Patient.Data.Entity;
using System.Collections.Generic;

namespace OPUSERP.Areas.Patient.Models
{
    public class ProfessionTypeViewModel
    {
        public int professionId { get; set; }
        public string professionName { get; set; }
        public string professionDescription { get; set; }

        public IEnumerable<ProfessionType> professionTypes { get; set; }
    }
}
