namespace OPUSERP.Areas.DMS.Models
{
    public class DocumentAccessViewModel
    {
        public int? Id { get; set; }
        public string actionType { get; set; }
        public int? actionTypeId { get; set; }
        public string documentType { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileType { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public int? moduleId { get; set; }
        public string hasEmployee { get; set; }
        public int? isViewed { get; set; }

    }
}
