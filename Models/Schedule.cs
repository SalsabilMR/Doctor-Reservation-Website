using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorReservation.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int AppointmentDurationInMinutes { get; set; }
        public bool  ScheduleStatus { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
