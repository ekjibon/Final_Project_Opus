using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.News
{
    [Table("NewsInfo", Schema = "Club")]
    public class NewsInfo : Base
    {
        public string title { get; set; }
        public string url { get; set; }
        public DateTime? date { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }
    }
}
