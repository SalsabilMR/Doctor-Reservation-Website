namespace DoctorReservation.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int AppointmentDurationInMinutes { get; set; }
        public Status ScheduleStatus { get; set; }
        public enum Status
        {
            Available,
            NotAvailable
        }

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
