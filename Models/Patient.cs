using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorReservation.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength( 20, ErrorMessage = "Name not more than 20 letters")]
        [MinLength(3, ErrorMessage = "Name not less than 3 letters")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(20, ErrorMessage = "Name not more than 20 letters")]
        [MinLength(3, ErrorMessage = "Name not less than 3 letters")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter your gender")]
        public Gender _Gender { get; set; }
        [Required(ErrorMessage = "Please enter your first Birth of date")]
        [DataType(DataType.Date)]
        public DateTime BirthOfDate { get; set; }
        [Required(ErrorMessage = "Please enter your mobile")]
        [RegularExpression(@"^01[0-2,5]{1}[0-9]{8}$", ErrorMessage = "Invalid egyptian number")]
        public string Mobile { get; set; }

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
