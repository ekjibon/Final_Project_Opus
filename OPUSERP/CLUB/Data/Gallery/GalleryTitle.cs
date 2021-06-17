using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Gallery
{
    [Table("GalleryTitle", Schema = "Club")]
    public class GalleryTitle:Base
    {
        public string title { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
        public string remarks { get; set; }

        public int type { get; set; } // 1 = Image ; 0 = Videos 
    }
}
