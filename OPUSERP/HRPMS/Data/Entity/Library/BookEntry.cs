using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Library
{
    [Table("BookEntry", Schema = "HR")]
    public class BookEntry : Base
    {
        public int? bookCategoryId { get; set; }
        public BookCategory bookCategory { get; set; }

        [Required]
        public string bookId { get; set; }

        public string workStation { get; set; }

        public string department { get; set; }

        [MaxLength(250)]
        public string bookName { get; set; }

        public string description { get; set; }

        [MaxLength(250)]
        public string writterName { get; set; }

        [MaxLength(100)]
        public string sbnNumber { get; set; }

        [MaxLength(250)]
        public string publisher { get; set; }

        [MaxLength(100)]
        public string price { get; set; }

        public string publishDate { get; set; }

        public string almiraNo { get; set; }

        public string selfNo { get; set; }

        public string quantity { get; set; }

        public string editionNo { get; set; }

        public string satus { get; set; }

        public string remark { get; set; }

        //For Type Managing 
        [MaxLength(100)]
        public string orgType { get; set; }
    }
}
