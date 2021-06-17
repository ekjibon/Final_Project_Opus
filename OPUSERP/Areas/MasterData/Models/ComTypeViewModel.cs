using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.MasterData.Models
{
    public class ComTypeViewModel
    {
        public int comTypeId { get; set; }

        [Required]
        public string communicationTypeName { get; set; }

        public IEnumerable<CommunicationType> communicationTypes { get; set; }
    }
}
