using OPUSERP.CLUB.Data.Event;
using Microsoft.AspNetCore.Http;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.Event.Models.Lang;

namespace OPUSERP.Areas.Event.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public int? participantLimit { get; set; }

        public int? category { get; set; }

        public int? containsDayLong { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string venue { get; set; }
        public string hiddenFile { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? deadline { get; set; }

        public string stime { get; set; }

        public string etime { get; set; }

        public string type { get; set; }
        public decimal? amount { get; set; }

        [Display(Name = "Event Photo")]
        public IFormFile empPhoto { get; set; }

        public decimal?[] qntPrice { get; set; }
        public int?[] head { get; set; }

        public EventLn fLang { get; set; }
        public IEnumerable<EventInformation> eventInformations { get; set; }

        public IEnumerable<EventCategory> eventCategories { get; set; }

        public IEnumerable<PaymentHead> paymentHeads { get; set; }

        public IEnumerable<ParticipantHead> participantHeads { get; set; }

        public IEnumerable<EventPerticipantHead> eventPerticipantHeads { get; set; }

        public IEnumerable<EventParticipants> eventParticipants { get; set; }

        public IEnumerable<EventRegister> eventRegisters { get; set; }

        public IEnumerable<EventData> eventDatas { get; set; }

        public EventData eventData { get; set; }

        public EventInformation eventInformation { get; set; }
    }
}
