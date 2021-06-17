using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Event
{
    [Table("ParticipantHead", Schema = "Club")]
    public class ParticipantHead:Base
    {
        public string name { get; set; }
        public string remarks { get; set; }
    }
}
