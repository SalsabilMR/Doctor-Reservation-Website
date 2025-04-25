using DoctorReservation.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DoctorReservation.Services
{
    
    public class ScheduleServices
    {
        DoctorReservationDBContext Context;
        public ScheduleServices(DoctorReservationDBContext Context) 
        {
            this.Context = Context;
        }
        public Doctor? GetDoctorByUserId(String UserId)
        {
            var doctor = Context.Doctors.FirstOrDefault(d=>d.ApplicationUserId == UserId);
            return doctor;
        }
        public int Create(Schedule schedule)
        {
            schedule.ScheduleStatus = true;
            Context.Schedules.Add(schedule);
            return Context.SaveChanges();
        }
        /********************************************************************/

    }
}
