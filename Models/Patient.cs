using System.Runtime.Serialization;

namespace DoctorReservation.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender _Gender { get; set; }
        public DateTime BirthOfDate { get; set; }
        public int Mobile { get; set; }

        public enum Gender
        {
            Male,Female
        }
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; } = new List<Appointment>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();


    }
}
