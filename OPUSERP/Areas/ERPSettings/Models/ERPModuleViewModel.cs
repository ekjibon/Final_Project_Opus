using OPUSERP.Data.Entity.Auth;
using System.Collections.Generic;

namespace OPUSERP.Areas.ERPSettings.Models
{
    public class ERPModuleViewModel
    {
        public int? moduleId { get; set; }

        public string moduleName { get; set; }

        public IEnumerable<ERPModule> eRPModules { get; set; }
    }
}
