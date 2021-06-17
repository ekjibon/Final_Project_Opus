using OPUSERP.Data.Entity;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CLUB.Data.Event
{
    [Table("EventInformation", Schema = "Club")]
    public class EventInformation : Base
    {

        //Foreign Reliation
        public int? paymentHeadId { get; set; }
        public PaymentHead paymentHead { get; set; }

        //Foreign Reliation
        public int? eventCategoryId { get; set; }
        public EventCategory eventCategory { get; set; }

        public string title { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }
        public string venue { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? deadline { get; set; }

        public int? participantLimit { get; set; }
        public string participantCategory { get; set; }
        public string type { get; set; }
        public decimal? amount { get; set; }
        public string url { get; set; }
        public string status { get; set; }

        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
