
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorReservation.Models
{
    public class DoctorReservationDBContext :IdentityDbContext
    {
        public DoctorReservationDBContext()
        {
        }
        public DoctorReservationDBContext(DbContextOptions<DoctorReservationDBContext> options) : base(options)
        {
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Doctor)
                .WithOne(d => d.ApplicationUser)
                .HasForeignKey<Doctor>(d => d.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Patient)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Patient>(p => p.ApplicationUserId);
        }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

    }
}
