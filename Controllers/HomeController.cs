using DoctorReservation.Models;
using DoctorReservation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static DoctorReservation.Models.Doctor;

namespace DoctorReservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoctorServices _doctorServices;

        public HomeController(ILogger<HomeController> logger , DoctorServices doctorServices)
        {
            _logger = logger;
            _doctorServices = doctorServices;
        }

        public IActionResult Index()
        {
            var doctors = _doctorServices.GetAll()?.ToList();

            //var specializations = _doctorServices.GetAll()
            //                       ?.Select(d => d.specialization)
            //                       .Distinct()
            //                       .ToList();

            //ViewBag.Specializations = specializations;
            return View(doctors);
        }
        public IActionResult SearchBySpecialization(Specialization specialization)
        {
            var doctors = _doctorServices.GetAll()?
                .Where(d => d.specialization == specialization)
                .ToList();
         
            return View("Index", doctors);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
