using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proekt1.Data;
using proekt1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace proekt1.Controllers
{
    //[Authorize(Roles ="admin,employee")]
    public class ReservationsController : Controller
    {
        private readonly proekt1Context _context;
        private readonly IEmailSender _emailSender;

        public ReservationsController(proekt1Context context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var proekt1Context = _context.Reservation.Include(r => r.Flight).Include(r => r.User);
            return View(await proekt1Context.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Flight)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create

        //[AllowAnonymous]
        public IActionResult Create(int? FlightID)
        {
            ViewData["FlightID"] = new SelectList(_context.Flight, "FlightID", "EndLocation");
            ViewData["UserEmail"] = new SelectList(_context.Set<Person>(), "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,FirstName,MiddleName,LastName,Email,EGN,Phone,TicketType,FlightID")] Reservation reservation)
        {
            var flight = await _context.Flight.FirstOrDefaultAsync(x => x.FlightID == reservation.FlightID);
            reservation.PlaneID = flight.PlaneID;
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                //AddReservation(flight.FlightID, reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewData["FlightID"] = new SelectList(_context.Flight, "FlightID", "EndLocation", reservation.FlightID);
            ViewData["UserEmail"] = new SelectList(_context.Set<Person>(), "Id", "Id", reservation.UserEmail);

            return View(reservation);
        }
        

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightID"] = new SelectList(_context.Flight, "FlightID", "EndLocation", reservation.FlightID);
            ViewData["UserEmail"] = new SelectList(_context.Set<Person>(), "Id", "Id", reservation.UserEmail);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationID,FirstName,MiddleName,LastName,Email,EGN,Phone,TicketType,FlightID")] Reservation reservation)
        {
            if (id != reservation.ReservationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationID))
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
            ViewData["FlightID"] = new SelectList(_context.Flight, "FlightID", "EndLocation", reservation.FlightID);
            ViewData["UserEmail"] = new SelectList(_context.Set<Person>(), "Id", "Id", reservation.UserEmail);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Flight)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationID == id);
        }
    }
}
