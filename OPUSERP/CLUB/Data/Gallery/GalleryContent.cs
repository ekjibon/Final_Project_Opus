using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Gallery
{
    [Table("GalleryContent", Schema = "Club")]
    public class GalleryContent:Base
    {
        public int galleryTitleId { get; set; }
        public GalleryTitle galleryTitle { get; set; }

        public string caption { get; set; }
        public string url { get; set; }
        public string remarks { get; set; }
    }
}
