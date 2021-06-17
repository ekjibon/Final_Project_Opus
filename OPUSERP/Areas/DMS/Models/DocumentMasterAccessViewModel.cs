namespace OPUSERP.Areas.DMS.Models
{
    public class DocumentMasterAccessViewModel
    {
        public int? Id { get; set; }
        public string documentName { get; set; }
        public int? documentCategoryId { get; set; }
        public string categoryName { get; set; }
        public string documentNo { get; set; }
        public string docCreateBy { get; set; }
        public string docCreateDate { get; set; }
        public int? leadsId { get; set; }
        public string leadName { get; set; }
        public int? employeeInfoId { get; set; }
        public string nameEnglish { get; set; }
        public string hasEmployee { get; set; }
        public int? isViewed { get; set; }

    }
}
