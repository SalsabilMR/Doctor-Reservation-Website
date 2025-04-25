using DoctorReservation.Models;
using DoctorReservation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DoctorReservation.Controllers
{
    public class ScheduleController : Controller
    {
        ScheduleServices scheduleServices;
        public ScheduleController(ScheduleServices scheduleServices)
        {
            this.scheduleServices = scheduleServices;
        }
        // GET: ScheduleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ScheduleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ScheduleController/Create
        public ActionResult Create()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var doctor = scheduleServices.GetDoctorByUserId(userId);
            if (doctor == null)
            {
                return Unauthorized();
            }
            var model = new Schedule
            {
                DoctorId = doctor.Id
            };

            return View(model);
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var doctor = scheduleServices.GetDoctorByUserId(userId);

            if (doctor == null)
            {
                return Unauthorized();
            }
            schedule.DoctorId = doctor.Id;

            if (ModelState.IsValid)
            {
                scheduleServices.Create(schedule);
                return RedirectToAction("Index", "Doctor");
            }

            return View(schedule);
        }

        // GET: ScheduleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ScheduleController/Edit/5
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

        // GET: ScheduleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ScheduleController/Delete/5
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
