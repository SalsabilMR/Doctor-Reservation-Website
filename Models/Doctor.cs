using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorReservation.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }
        public int Mobile { get; set; }
        public int Fees { get; set; }
        public string? specialization { get; set; }
        public string? Description { get; set; }
        
        public string? CertificatePath { get; set; } //save file name in DB
        [NotMapped]
        public IFormFile? Certificate { get; set; } //recieve file when upload

        public string? ImagePath { get; set; } //save file name in DB
        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; } = new List<Schedule>();
        public virtual ICollection<Appointment> Appointment { get; set; } = new List<Appointment>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();



    }
}
