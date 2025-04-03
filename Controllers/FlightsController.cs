using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proekt1.Data;
using proekt1.Models;

namespace proekt1.Controllers
{
    //promqna
    [Authorize(Roles = "admin,employee")]
    public class FlightsController : Controller
    {
        private readonly proekt1Context _context;

        public FlightsController(proekt1Context context)
        {
            _context = context;
        }

        // GET: Flights
        //promqna
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin") || User.IsInRole("employee"))
                return View(await _context.Flight.ToListAsync());
            else
            {
                var availableFlights = await _context.Flight.ToListAsync();
                foreach(var flight in availableFlights)
                {
                    if(flight.Reservations.Count == flight.Plane.MaxSeats)
                        availableFlights.Remove(flight);
                }
                return View(availableFlights);
            }
                
        }

        // GET: Flights/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        //rpomqna
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("FlightID,StartLocation,EndLocation,StartDateTime,EndDateTime,PilotName,PlaneID")] Flight flight)
        {
            flight.Reservations = new List<Reservation>();
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("FlightID,StartLocation,EndLocation,StartDateTime,EndDateTime,PilotName,PlaneID")] Flight flight)
        {
            if (id != flight.FlightID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                _context.Flight.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.FlightID == id);
        }

        // GET: Flights/Reserve/5
        //[AllowAnonymous]
        //public async Task<IActionResult> Reserve(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var flight = await _context.Flight
        //        .FirstOrDefaultAsync(m => m.FlightID == id);
        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }
        //    //return View(flight);
        //    return RedirectToAction("Create", "Reservations", new { flightId = id });
        //}

        // POST: Flights/Reserve/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public async Task<IActionResult> Reserve(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var flight = await _context.Flight
        //        .FirstOrDefaultAsync(m => m.FlightID == id);
        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }

        //    return RedirectToAction("Create", "Reservations", new {flightId = id});
        //}
    }
}
