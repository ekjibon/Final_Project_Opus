using OPUSERP.CLUB.Data.Notice;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Notice.Models.Lang;

namespace OPUSERP.Areas.Notice.Models
{
    public class NoticeInfoViewModel
    {
        public int? Id { get; set; }

        public string subject { get; set; }

        public string notice { get; set; }

        public string noticeType { get; set; }

        public int[] author { get; set; }

        public IFormFile attachment { get; set; }

        public NoticeLn fLang { get; set; }
        public IEnumerable<NoticeInfo> noticeInfos { get; set; }
        public NoticeInfo noticeInfo { get; set; }
        public IEnumerable<NoticeType> noticeTypes { get; set; }

        public IEnumerable<NoticeAuthor> noticeAuthors { get; set; }

        public IEnumerable<RlnNoticeAuthor> rlnNoticeAuthors { get; set; }
    }
}
