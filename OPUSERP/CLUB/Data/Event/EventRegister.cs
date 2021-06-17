using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Event
{
    [Table("EventRegister", Schema = "Club")]
    public class EventRegister:Base
    {
        //Foreign Reliation
        public int? employeeId { get; set; }
        public MemberInfo employee { get; set; }

        //Foreign Reliation
        public int? eventInformationId { get; set; }
        public EventInformation eventInformation { get; set; }

        //Foreign Reliation
        public int? eventPerticipantHeadId { get; set; }
        public EventPerticipantHead eventPerticipantHead { get; set; }

        public int? count { get; set; }

        public int? status { get; set; }

        public string transectionId { get; set; }

        public string remarks { get; set; }
    }
}
