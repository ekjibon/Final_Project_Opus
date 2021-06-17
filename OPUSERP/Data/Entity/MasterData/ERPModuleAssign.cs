using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Data.Entity.MasterData
{
    public class ERPModuleAssign:Base
    {
        public int? companyId { get; set; }
        public Company company { get; set; }

        public string roleId { get; set; }
    }
}
