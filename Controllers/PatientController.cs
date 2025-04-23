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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient Patient)
        {
            if (ModelState.IsValid)

            {
                PatientServices.Create(Patient);
                return RedirectToAction("Create");
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
                return RedirectToAction("Index");
            }

            return View();
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
