using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("Document", Schema = "CRM")]
    public class Document:Base 
    {
        public int? contactsId { get; set; }
        public Contacts contacts { get; set; }
        public int? DocSubjectId { get; set; }

        public string FileLocation { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }
    }
}
