using IT2166_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT2166_Assignment.Controllers
{
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
    }
}
