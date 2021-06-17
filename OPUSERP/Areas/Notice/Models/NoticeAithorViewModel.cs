using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.Notice.Models.Lang;
using OPUSERP.CLUB.Data.Notice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Notice.Models
{
    public class NoticeAithorViewModel
    {
        public int Id { get; set; }
        public int memberId { get; set; }
        public string memberName { get; set; }

        [Display(Name = "Author Sign Photo")]
        public IFormFile empPhoto { get; set; }

        public string designation { get; set; }

        public IEnumerable<NoticeAuthor> noticeAuthors { get; set; }
        public NoticeLn fLang { get; set; }
    }
}
