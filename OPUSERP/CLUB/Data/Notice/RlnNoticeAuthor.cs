using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Notice
{
    [Table("RlnNoticeAuthor", Schema = "Club")]
    public class RlnNoticeAuthor : Base
    {
        public int? noticeInfoId { get; set; }
        public NoticeInfo noticeInfo { get; set; }

        public int? noticeAuthorId { get; set; }
        public NoticeAuthor noticeAuthor { get; set; }
    }
}
