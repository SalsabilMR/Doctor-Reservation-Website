using DoctorReservation.Models;

namespace DoctorReservation.Services
{
    public class PatientServices
    {
        DoctorReservationDBContext Context;
        public PatientServices(DoctorReservationDBContext Context)
        {
            this.Context = Context;
        }

        public List<Patient>? GetAll()
        {
            var Patients = Context.Patients.ToList();
            return Patients;
        }
        /********************************************************************/
        public Patient? GetDetails(int id)
        {
            var Patient = Context.Patients.SingleOrDefault(x => x.Id == id);
            return Patient;
        }
        /********************************************************************/
        public int Create(Patient patient)
        {
            Context.Patients.Add(patient);
            return Context.SaveChanges();
        }
        /********************************************************************/
        public int Edit(Patient patient)
        {
            var pat = Context.Patients.FirstOrDefault(x => x.Id == patient.Id);
            if (pat != null)
            {
                pat.FirstName = patient.FirstName;
                pat.LastName = patient.LastName;
                pat._Gender = patient._Gender;
                pat.BirthOfDate = patient.BirthOfDate;
                pat.Mobile = patient.Mobile;
            }
            return Context.SaveChanges();
        }

        public int Delete(int id)
        {
            var patient = Context.Patients.FirstOrDefault(x => x.Id == id);
            if (patient != null)
            Context.Patients.Remove(patient);
            return Context.SaveChanges();
        }
    }
}
