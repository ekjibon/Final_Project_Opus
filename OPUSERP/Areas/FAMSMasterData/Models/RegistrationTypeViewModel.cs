using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.FAMSMasterData.Models
{
    public class RegistrationTypeViewModel
    {
        public int Id { get; set; }

       
        public string RegistrationTypeName { get; set; }

        public IEnumerable<RegistrationType> registrationTypes { get; set; }
    }
}
