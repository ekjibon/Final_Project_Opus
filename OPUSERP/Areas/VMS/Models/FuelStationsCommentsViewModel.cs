using System.Collections.Generic;
using OPUSERP.VMS.Data.Entity.FuelStation;


namespace OPUSERP.Areas.VMS.Models
{
    public class FuelStationsCommentsViewModel
    {
        public int? commentsId { get; set; }

        public int? fuelstationInfoId { get; set; }

        public string titles { get; set; }

        public string comments { get; set; }

        public virtual IEnumerable<FuelStationComment> vehicleComments { get; set; }
    }
}
