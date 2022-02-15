using IT2166_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT2166_Assignment.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDBContext _context;
        public BookingController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Booking> bookings;
            bookings = _context.Bookings.ToList();
            return View(bookings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Booking booking = new Booking();
            return View(booking);
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Booking booking = _context.Bookings
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /*public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            var getbooking = await _context.FindAsync(Id);
            return View(getbooking);
        }*/

        public IActionResult Details(int Id)
        {
            Booking booking = _context.Bookings
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(booking);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Booking booking = _context.Bookings
                .Where(a => a.Id == Id).FirstOrDefault();
            return View(booking);
        }

        [HttpPost]
        public IActionResult Delete(Booking booking)
        {
            _context.Attach(booking);

            _context.Entry(booking).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
