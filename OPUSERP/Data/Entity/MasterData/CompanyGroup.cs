namespace OPUSERP.Data.Entity.Master
{
    public class CompanyGroup : Base
    {
        public string groupName { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }
    }
}
