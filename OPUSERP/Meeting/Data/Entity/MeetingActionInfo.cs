using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Meeting.Data.Entity
{
    [Table("MeetingActionInfo", Schema = "MMS")]
    public class MeetingActionInfo:Base
    {
        public string description { get; set; }
    }
}
