using DoctorReservation.Models;
using DoctorReservation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Numerics;

namespace DoctorReservation.Controllers
{

    public class DoctorController : Controller
    {
        DoctorServices DocServices;
        public DoctorController(DoctorServices DocServices)
        {
           this.DocServices = DocServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var doctors = DocServices.GetAll();
            if (doctors == null)
            {
                return NotFound();
            }
            return View(doctors);
        }
        public IActionResult Details(int id)
        {
            var doctor = DocServices.GetDetails(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        public IActionResult Create(string userId)
        {
            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if(ModelState.IsValid)

            {
                DocServices.Create(doctor);
                return RedirectToAction("Profile", new { userId = doctor.ApplicationUserId });
            }

            return View(doctor);
            
        }

        public IActionResult Edit(int id)
        {
            var doc = DocServices.GetDetails(id);
            return View(doc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DocServices.Edit(doctor);
                return RedirectToAction("Profile", "Doctor", new { userId = doctor.ApplicationUserId });
            }

            return View();
        }

        public IActionResult Profile(string userId)
        {
            var doctor = DocServices.GetAll()?.FirstOrDefault(d => d.ApplicationUserId == userId);
  
            if (doctor == null)
            {
                return RedirectToAction("Create", new { userId = userId });
            }

            return View(doctor);
        }

        public IActionResult Delete(int id)
        {

            var doctor = DocServices.GetDetails(id);  
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
                DocServices.Delete(id);
                return RedirectToAction("Index");
            }
            
           
            return View();
            
        }
    }
}
