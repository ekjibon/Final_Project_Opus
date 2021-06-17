using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Notice
{
    [Table("NoticeType", Schema = "Club")]
    public class NoticeType:Base
    {
        public string name { get; set; }
    }
}
