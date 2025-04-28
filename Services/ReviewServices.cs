using DoctorReservation.Models;

namespace DoctorReservation.Services
{
    public class ReviewServices
    {
        DoctorReservationDBContext Context;
        public ReviewServices(DoctorReservationDBContext Context)
        {
            this.Context = Context;
        }
        public Doctor? GetDoctorByUserId(String UserId)
        {
            var doctor = Context.Doctors.FirstOrDefault(d => d.ApplicationUserId == UserId);
            return doctor;
        }
        public Patient? GetPatientByUserId(String UserId)
        {
            var patient = Context.Patients.FirstOrDefault(d => d.ApplicationUserId == UserId);
            return patient;
        }
        public int Create(Review review)
        {
            review.Rating = 0;  //----------------
            review.ReviewDate = DateTime.Now;
            review.Comment = ""; //---------------

            Context.Reviews.Add(review);
            return Context.SaveChanges();
        }
        public int Edit(Review review)
        {
            var pat = Context.Reviews.FirstOrDefault(x => x.Id == review.Id);
            if (pat != null)
            {
                pat.Rating = review.Rating;
                pat.ReviewDate = review.ReviewDate;
                pat.Comment = review.Comment;
            }
            return Context.SaveChanges();
        }
        public Review? GetDetails(int id)
        {
            var Review = Context.Reviews.SingleOrDefault(x => x.Id == id);
            return Review;
        }
    }
}
