using DoctorReservation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DoctorReservation.Controllers
{

    public class DoctorController : Controller
    {
        DoctorReservationDBContext Context; //= new DoctorReservationDBContext();
        public DoctorController(DoctorReservationDBContext Context)
        {
            this.Context = Context;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var doc = Context.Doctors.FirstOrDefault(x => x.Id == id);
            return View(doc);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctor doctor)
        {
            if(ModelState.IsValid)

            {
                if (doctor.Image != null && doctor.Image.Length > 0)
                {
                    // Image Name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Image.FileName);

                    // Full Path wwwroot/uploads
                    string fullPath = Path.Combine("wwwroot/uploads/Images/", fileName);

                    // Save Image
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        doctor.Image.CopyTo(stream);
                    }

                    // Save path in Database
                    doctor.ImagePath = "uploads/Images/" + fileName;
                }

                if (doctor.Certificate != null && doctor.Certificate.Length > 0)
                {
                    // File Name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctor.Certificate.FileName);

                    // Full Path wwwroot/uploads
                    string fullPath = Path.Combine("wwwroot/uploads/Certificates/", fileName);

                    // Save File
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        doctor.Certificate.CopyTo(stream);
                    }

                    // Save path in Database
                    doctor.CertificatePath = "uploads/Certificates/" + fileName;
                }

                Context.Doctors.Add(doctor);
                Context.SaveChanges();

                return RedirectToAction("Index");
            }
            
                return View();
            
        }

        public ActionResult Edit(int id)
        {
            var doc = Context.Doctors.FirstOrDefault(x => x.Id == id);
            return View(doc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Doctor doctor, IFormFile NewImage)
        {
            if(ModelState.IsValid)
            {
                var doc = Context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
                doc.FirstName = doctor.FirstName;
                doc.LastName = doctor.LastName;
                doc.City = doctor.City;
                doc.Location = doctor.Location;
                doc.Email = doctor.Email;
                doc.Mobile = doctor.Mobile;
                doc.Fees = doctor.Fees;
                doc.specialization = doctor.specialization;
                doc.Description = doctor.Description;


                // لو فيه صورة جديدة
                if (NewImage != null && NewImage.Length > 0)
                {
                    // حذف الصورة القديمة من السيرفر (اختياري)
                    if (!string.IsNullOrEmpty(doc.ImagePath))
                    {
                        var oldPath = Path.Combine("wwwroot", doc.ImagePath);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // رفع الصورة الجديدة
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    string fullPath = Path.Combine("wwwroot/uploads/Images/", fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        doc.Image.CopyToAsync(stream);
                    }

                    doc.ImagePath = "uploads/Images/" + fileName;
                }

                Context.SaveChanges();
          

                return RedirectToAction("Index");
            }
                
                return View();
            
        }

        // GET: DoctorController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
