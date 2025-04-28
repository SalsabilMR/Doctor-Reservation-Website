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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var patient = ReviewServices.GetPatientByUserId(userId);

            if (patient == null)
            {
                return Unauthorized();
            }
            review.Id = patient.Id;

            if (ModelState.IsValid)
            {
                ReviewServices.Create(review);
                return RedirectToAction("Index", "Doctor");
            }
            return View(review);
        }

        public IActionResult Edit(int id)
        {
            var Review = ReviewServices.GetDetails(id);
            return View(Review);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        public ActionResult Delete(int id)
        {
            return View();
        }
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
