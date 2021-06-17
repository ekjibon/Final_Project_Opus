using OPUSERP.Areas.Gallery.Models.Lang;
using OPUSERP.CLUB.Data.Gallery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Gallery.Models
{
    public class GalleryTitleViewModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public string remarks { get; set; }
        public int type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        public GalleryLn fLang { get; set; }
        public IEnumerable<GalleryTitle> galleryTitles { get; set; }
    }
}
