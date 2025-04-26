using Microsoft.AspNetCore.Mvc;
using DoctorReservation.Models;
using DoctorReservation.Services;

namespace DoctorReservation.Controllers
{
    public class ReviewController : Controller
    {
        ReviewServices ReviewServices;
        public ReviewController(ReviewServices ReviewServices)
        {
            this.ReviewServices = ReviewServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var Review = ReviewServices.GetDetails(id);
            if (Review == null)
            {
                return NotFound();
            }
            return View(Review);
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            ViewBag.UserId = userId;
            return View();
        }
        public IActionResult Edit(int id)
        {
            var Review = ReviewServices.GetDetails(id);
            return View(Review);
        }
    }
}
