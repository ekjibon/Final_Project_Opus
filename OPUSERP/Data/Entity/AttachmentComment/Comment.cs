using OPUSERP.Data.Entity.Auth;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Data.Entity.AttachmentComment
{
    public class Comment:Base
    {
        [MaxLength(250)]
        public string actionType { get; set; }
        public int? actionTypeId { get; set; }
        [MaxLength(250)]
        public string title { get; set; }
        public string comment { get; set; }
        public int? moduleId { get; set; }
        public ERPModule module { get; set; }
    }
}
