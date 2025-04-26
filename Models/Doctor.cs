using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorReservation.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter your first name")]
        [StringLength(20, ErrorMessage = "Name not more than 20 letters")]
        [MinLength(3, ErrorMessage = "Name not less than 3 letters")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(20, ErrorMessage = "Name not more than 20 letters")]
        [MinLength(3, ErrorMessage = "Name not less than 3 letters")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter your city")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Please enter your location")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter your Mobile")]
        [Phone(ErrorMessage = "Invalid Number")]
        public int Mobile { get; set; }
        [Required(ErrorMessage = "Please enter your fees")]
        public int Fees { get; set; }
        [Required(ErrorMessage = "Please enter your specialization")]
        public string? specialization { get; set; }
        [Required(ErrorMessage = "Please enter your Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter your Certificate")]
        public string? CertificatePath { get; set; } //save file name & file path in DB
        [NotMapped]
        public IFormFile? Certificate { get; set; } //recieve file when upload

        [Required(ErrorMessage = "Please enter your Image")]
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
