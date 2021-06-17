using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelCommentsViewModel
    {
        public int? commentsId { get; set; }

        public int fuelInformationId { get; set; }

        public string titles { get; set; }

        public string comments { get; set; }

        public virtual IEnumerable<FuelComment>  fuelComments { get; set; }
    }
}
