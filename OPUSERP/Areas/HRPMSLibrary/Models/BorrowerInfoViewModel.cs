using OPUSERP.Areas.HRPMSLibrary.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSLibrary.Models
{
    public class BorrowerInfoViewModel
    {
        public int id { get; set; }

        public string bookId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string borrowerName { get; set; }

        public DateTime? borrowDate { get; set; }

        public DateTime? returnDate { get; set; }

        public string designation { get; set; }

        public string bookEntryName { get; set; }

        public string noOfCopy { get; set; }

        public BookLn fLang { get; set; }

        public IEnumerable<BorrowerInfo> borrowerInfos { get; set; }

        public BorrowerInfo borrowerInfo { get; set; }

        public IEnumerable<BookEntry> bookEntries { get; set; }

        public BookEntry bookEntry { get; set; }

    }
}
