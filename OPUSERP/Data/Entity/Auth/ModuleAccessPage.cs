namespace OPUSERP.Data.Entity.Auth
{
    public class ModuleAccessPage : Base
    {
        public int? eRPModuleId { get; set; }
        public ERPModule eRPModule { get; set; }

      

        public string applicationRoleId { get; set; }
        public ApplicationRole applicationRole { get; set; }
    }
}
