namespace OPUSERP.Areas.HRPMSAttendence.Models
{
    public class AttendanceViewModelApi
    {
        public string empName { get; set; }

        public string checkInDate { get; set; }
        public string checkOutDate { get; set; }
        public string checkInTime { get; set; }
        public string checkOutTime { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }

        public string latitudeIn { get; set; }
        public string latitudeOut { get; set; }
        public string longitudeIn { get; set; }
        public string longitudeOut { get; set; }

    }
}
