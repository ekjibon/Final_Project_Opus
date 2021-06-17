using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class ProjectViewModel
    {
        public TaskProject taskProject { get; set; }

        public IEnumerable<SectorViewModel> sectorViewModels { get; set; }

    }
}
