using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "admin")]
    public class PlanesController : Controller
    {
        private readonly proekt1Context _context;

        public PlanesController(proekt1Context context)
        {
            _context = context;
        }

        // GET: Planes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plane.ToListAsync());
        }

        // GET: Planes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _context.Plane
                .FirstOrDefaultAsync(m => m.PlaneID == id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        //// GET: Planes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Planes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PlaneID,Company,MaxSeats,MaxBusinessSeats")] Plane plane)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(plane);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(plane);
        //}

        //// GET: Planes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var plane = await _context.Plane.FindAsync(id);
        //    if (plane == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(plane);
        //}

        //// POST: Planes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PlaneID,Company,MaxSeats,MaxBusinessSeats")] Plane plane)
        //{
        //    if (id != plane.PlaneID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(plane);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PlaneExists(plane.PlaneID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(plane);
        //}

        //// GET: Planes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var plane = await _context.Plane
        //        .FirstOrDefaultAsync(m => m.PlaneID == id);
        //    if (plane == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(plane);
        //}

        //// POST: Planes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var plane = await _context.Plane.FindAsync(id);
        //    if (plane != null)
        //    {
        //        _context.Plane.Remove(plane);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PlaneExists(int id)
        //{
        //    return _context.Plane.Any(e => e.PlaneID == id);
        //}
    }
}
