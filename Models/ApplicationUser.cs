using Microsoft.AspNetCore.Identity;

namespace DoctorReservation.Models
{
    public class ApplicationUser :IdentityUser
    {

        public virtual Doctor? Doctor { get; set; }
        public virtual  Patient? Patient { get; set; }

    }
}
