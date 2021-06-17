using System.ComponentModel.DataAnnotations.Schema;
using OPUSERP.Data.Entity;

namespace OPUSERP.CLUB.Data.Event
{
    [Table("EventPerticipantHead", Schema = "Club")]
    public class EventPerticipantHead:Base
    {
        //Foreign Reliation
        public int? eventInformationId { get; set; }
        public EventInformation eventInformation { get; set; }

        //Foreign Reliation
        public int? participantHeadId { get; set; }
        public ParticipantHead participantHead { get; set; }

        public decimal? price { get; set; }

        public string remarks { get; set; }
    }
}
