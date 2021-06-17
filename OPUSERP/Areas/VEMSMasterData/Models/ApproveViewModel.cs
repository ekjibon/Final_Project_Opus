using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VEMSMasterData.Models
{
    public class ApproveViewModel
    {
        public int? companyId { get; set; }
        public int? approveType { get; set; }

        public RegistrationForm registrationForm { get; set; }
        public IEnumerable<RegistrationForm> pendingRegistrationForms { get; set; }
        public IEnumerable<RegistrationForm> approvedRegistrationForms { get; set; }
        public IEnumerable<RegistrationForm> rejectedRegistrationForms { get; set; }
    }
}
