using DoctorReservation.Models;

namespace DoctorReservation.Services
{
    
    public class ScheduleServices
    {
        DoctorReservationDBContext Context;
        public ScheduleServices(DoctorReservationDBContext Context) 
        {
            this.Context = Context;
        }
        public int Create(Schedule schedule)
        {
            Context.Schedules.Add(schedule);
            return Context.SaveChanges();
        }
        /********************************************************************/

    }
}
