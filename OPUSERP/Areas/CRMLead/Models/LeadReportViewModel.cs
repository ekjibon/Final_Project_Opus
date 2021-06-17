namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadReportViewModel
    {
        public int Id { get; set; }
        public string leadName { get; set; }
        public string leadNumber { get; set; }
        public string leadOwner { get; set; }
        public string leadShortName { get; set; }
        public string sectorName { get; set; }
        public string fiTypeName { get; set; }
        public string ownerShipTypeName { get; set; }
        public string sourceName { get; set; }
        public string createdAt { get; set; }
        public string address { get; set; }
    }
}
