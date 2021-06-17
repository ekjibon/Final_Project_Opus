using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class NomineeFundViewModel
    {
        public int? nomineeFundId { get; set; }

        [Required]
        public string nomineeFundName { get; set; }


        public IEnumerable<NomineeFund> nomineeFunds { get; set; }
    }
}
