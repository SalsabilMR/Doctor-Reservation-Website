using DoctorReservation.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;

namespace DoctorReservation.Services
{
    public class DoctorServices
    {
        DoctorReservationDBContext Context;
        public DoctorServices(DoctorReservationDBContext Context)
        {
            this.Context = Context;
        }

        public List<Doctor>? GetAll()
        { 
            var doctors = Context.Doctors.ToList();
            return doctors;
        }
        /********************************************************************/
        public Doctor? GetDetails(int id)
        { 
            var doctor = Context.Doctors.SingleOrDefault(x => x.Id == id);
            return doctor;
        }
        /********************************************************************/
        public int Create(Doctor doctor)
        {

            if (doctor.Image != null && doctor.Image.Length > 0)
            {
                // Image Name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Image.FileName);

                // Full Path wwwroot/uploads
                string fullPath = Path.Combine("wwwroot/Uploads/Images/", fileName);

                // Save Image
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    doctor.Image.CopyTo(stream);
                }

                // Save path in Database
                doctor.ImagePath = "Uploads/Images/" + fileName;
            }
            else
            {
                doctor.ImagePath = "Uploads/Images/default-user.png";
            }

            if (doctor.Certificate != null && doctor.Certificate.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Certificate.FileName);
                string fullPath = Path.Combine("wwwroot/Uploads/Certificates/", fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    doctor.Certificate.CopyTo(stream);
                }
                doctor.CertificatePath = "Uploads/Certificates/" + fileName;
            }
            else
            {
                doctor.CertificatePath = "Uploads/Certificates/Certificate.png";
            }


            Context.Doctors.Add(doctor);
            return Context.SaveChanges();
        } 
        /********************************************************************/
        public int Edit(Doctor doctor)
        {
            var doc = Context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
            if (doc != null)
            {
                doc.FirstName = doctor.FirstName;
                doc.LastName = doctor.LastName;
                doc.City = doctor.City;
                doc.Location = doctor.Location;
                doc.Email = doctor.Email;
                doc.Mobile = doctor.Mobile;
                doc.Fees = doctor.Fees;
                doc.specialization = doctor.specialization;
                doc.Description = doctor.Description;

                if (doctor.Image != null)
                {
                    // Delete old image
                    if (!string.IsNullOrEmpty(doctor.ImagePath))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", doctor.ImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    // save new image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Image.FileName);
                    string fullPath = Path.Combine("wwwroot/Uploads/Images/", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        doctor.Image.CopyTo(stream);
                    }
                    doc.ImagePath = "Uploads/Images/" + fileName;
                }
                if (doctor.Certificate != null)
                {
                    // Delete old Certificate
                    if (!string.IsNullOrEmpty(doctor.CertificatePath))
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", doctor.CertificatePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    // save new Certificate
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Certificate.FileName);
                    string fullPath = Path.Combine("wwwroot/Uploads/Images/", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        doctor.Certificate.CopyTo(stream);
                    }
                    doc.CertificatePath = "Uploads/Images/" + fileName;
                }

                
            }
            return Context.SaveChanges();
        }

        public int Delete(int id)
        {
            var doctor = Context.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctor != null)
            {
                if (!string.IsNullOrEmpty(doctor.ImagePath))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", doctor.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                Context.Doctors.Remove(doctor);
            }
            
            return Context.SaveChanges();
        }
    }
}
