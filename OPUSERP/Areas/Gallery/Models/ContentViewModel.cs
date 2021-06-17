using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.Gallery.Models.Lang;
using OPUSERP.CLUB.Data.Gallery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Gallery.Models
{
    public class ContentViewModel
    {
        public int galleryTitleId { get; set; }
        public string caption { get; set; }

        public string videoId { get; set; }

        [Display(Name = "Event Photo")]
        public IFormFile empPhoto { get; set; }

        public string remarks { get; set; }

        public GalleryLn fLang { get; set; }
        public IEnumerable<GalleryTitle> galleryTitles { get; set; }

        public IEnumerable<GalleryContent> galleryContents { get; set; }
    }
}
