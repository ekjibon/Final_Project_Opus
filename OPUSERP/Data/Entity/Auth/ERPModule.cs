namespace OPUSERP.Data.Entity.Auth
{
    public class ERPModule:Base
    {
        public string moduleName { get; set; }

        public string moduleNameBN { get; set; }

        public int? shortOrder { get; set; }

        public string isTeam { get; set; }
    }
}
