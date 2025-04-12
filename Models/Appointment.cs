namespace DoctorReservation.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Status AppointmentStatus { get; set; }
        public enum Status
        {
            Reserved,
            Cancelled,
            Completed,
            Pending

        }

        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Schedule? Schedule { get; set; }
    }
}
