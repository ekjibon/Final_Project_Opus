using OPUSERP.Data.Entity.Auth;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Data.Entity.AttachmentComment
{
    public class DocumentPhotoAttachment : Base
    {
        [MaxLength(250)]
        public string actionType { get; set; }
        public int? actionTypeId { get; set; }
        [MaxLength(250)]
        public string documentType { get; set; }
        [MaxLength(350)]
        public string fileName { get; set; }
        [MaxLength(350)]
        public string filePath { get; set; }
        [MaxLength(250)]
        public string fileType { get; set; }
        [MaxLength(350)]
        public string subject { get; set; }
        [MaxLength(450)]
        public string description { get; set; }
        public int? moduleId { get; set; }
        public ERPModule module { get; set; }
    }
}
