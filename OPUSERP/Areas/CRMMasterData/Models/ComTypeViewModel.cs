using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class ComTypeViewModel
    {
        public int comTypeId { get; set; }

        [Required]
        public string communicationTypeName { get; set; }

        public IEnumerable<CommunicationType> communicationTypes { get; set; }
    }
}
