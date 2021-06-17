using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.News.Models.Lang;
using OPUSERP.CLUB.Data.News;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.News.Models
{
    public class NewsInfoViewModel
    {
        public int Id { get; set; }

        public string title { get; set; }

        [Display(Name = "Event Photo")]
        public IFormFile empPhoto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public string description { get; set; }

        public NewsLn fLang { get; set; }
        public IEnumerable<NewsData> newsDatas { get; set; }

        public IEnumerable<NewsInfo> newsInfos { get; set; }

        public NewsInfo newsInfo { get; set; }
    }
}
