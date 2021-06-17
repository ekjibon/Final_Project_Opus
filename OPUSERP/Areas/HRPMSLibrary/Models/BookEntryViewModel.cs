using OPUSERP.Areas.HRPMSLibrary.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Library;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLibrary.Models
{
    public class BookEntryViewModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string categoryId { get; set; }

        [Required]
        [Display(Name = "Book Id")]
        public string bookId { get; set; }
        public string workStation { get; set; }

        public string bookCategoey { get; set; }

        public string department { get; set; }

        public string bookName { get; set; }

        public string description { get; set; }

        public string writterName { get; set; }

        public string sbnNumber { get; set; }

        public string editionNo { get; set; }        

        public string publisher { get; set; }

        public string publishDate { get; set; }

        public string almiraNo { get; set; }

        public string selfNo { get; set; }

        public string quantity { get; set; }

        public string status { get; set; }

        public string price { get; set; }

        public string remark { get; set; }            

        public BookLn fLang { get; set; }

        public IEnumerable<BookCategory> bookCategories { get; set; }

        public BookCategory bookCategory { get; set; }

        public IEnumerable<BookEntry> bookEntries { get; set; }

        public BookEntry bookEntry { get; set; }

        public IEnumerable<BorrowerInfo> borrowerInfos { get; set; }

        public BorrowerInfo borrowerInfo { get; set; }
    }
}
