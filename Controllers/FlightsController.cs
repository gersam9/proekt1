using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchTerm)
        {
            //if(User.IsInRole("admin") || User.IsInRole("emoloyee"))
            //{
            var flights = from n in _context.Flight.Include(f => f.Plane) select n; // Get all notes

            if (!string.IsNullOrEmpty(searchTerm))
            {
                flights = flights.Where(n => n.StartLocation.Contains(searchTerm) || n.EndLocation.Contains(searchTerm) || n.StartDateTime.ToString().Contains(searchTerm) || n.EndDateTime.ToString().Contains(searchTerm));
            }
            ViewData["SearchTerm"] = searchTerm;
            //var proekt1Context = _context.Flight.Include(f => f.Plane);
                return View(await flights.ToListAsync());
            //}
            //else
            //{
            //    var proekt1Context = _context.Flight.Include(f => f.Plane);
            //    var availableFlights = new List<Flight>();
            //    foreach(var flight in proekt1Context)
            //    {
            //        if(flight.Reservations.Count != 0)
            //        {
            //            if(flight.Reservations.Count != flight.Plane.MaxSeats + flight.Plane.MaxBusinessSeats)
            //            {
            //                availableFlights.Add(flight);
            //            }
            //        }
            //    }
            //    return View(availableFlights);
            //}
            
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
                .Include(f => f.Plane)
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(m => m.FlightID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["PlaneID"] = new SelectList(_context.Plane, "PlaneID", "PlaneID");
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
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaneID"] = new SelectList(_context.Plane, "PlaneID", "PlaneID", flight.PlaneID);
            return View(flight);
        }

        // GET: Flights/Edit/5
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
            ViewData["PlaneID"] = new SelectList(_context.Plane, "PlaneID", "PlaneID", flight.PlaneID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["PlaneID"] = new SelectList(_context.Plane, "PlaneID", "PlaneID", flight.PlaneID);
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
                .Include(f => f.Plane)
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
    }
}
