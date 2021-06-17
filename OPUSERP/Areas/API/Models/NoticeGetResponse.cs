using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.API.Models
{
    public class NoticeGetResponse
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string pdfUrl { get; set; }
        public string attatchment { get; set; }
    }
}
