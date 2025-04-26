using DoctorReservation.Models;
using DoctorReservation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorReservation.Controllers
{
    public class PatientController : Controller
    {
        PatientServices PatientServices;
        public PatientController(PatientServices PatientServices)
        {
            this.PatientServices = PatientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var Patients = PatientServices.GetAll();
            if (Patients == null)
            {
                return NotFound();
            }
            return View(Patients);
        }
        public IActionResult Details(int id)
        {
            var Patient = PatientServices.GetDetails(id);
            if (Patient == null)
            {
                return NotFound();
            }
            return View(Patient);
        }
        [HttpGet]
        public IActionResult Create(string userId)
        {

            ViewBag.UserId = userId;
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient Patient)
        {
            if (ModelState.IsValid)

            {
                PatientServices.Create(Patient);
                return RedirectToAction("Profile", "Patient", new { userId = Patient.ApplicationUserId });
            }

            return View(Patient);

        }

        public IActionResult Edit(int id)
        {
            var Patient = PatientServices.GetDetails(id);
            return View(Patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient Patient)
        {
            if (ModelState.IsValid)
            {
                PatientServices.Edit(Patient);
                return RedirectToAction("Profile");
            }

            return View();
        }

        public IActionResult Profile(string userId)
        {
            var patient = PatientServices.GetAll()?.FirstOrDefault(p => p.ApplicationUserId == userId);

            if (patient == null)
            {
               
                return RedirectToAction("Create", new { userId = userId });
            }

            return View(patient);
        }
  

        public IActionResult Delete(int id)
        {

            var Patient = PatientServices.GetDetails(id);
            if (Patient != null)
            {
                PatientServices.Delete(id);
                return RedirectToAction("Index");
            }


            return View();

        }
    }
}
