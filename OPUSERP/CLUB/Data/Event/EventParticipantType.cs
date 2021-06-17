using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Event
{
    [Table("EventParticipantType", Schema = "Club")]
    public class EventParticipantType:Base
    {
        //Foreign Reliation
        public int? eventInformationId { get; set; }
        public EventInformation eventInformation { get; set; }

        //Foreign Reliation
        public int? participantTypeId { get; set; }
        public ParticipantType participantType { get; set; }

        public string remarks { get; set; }
    }
}
