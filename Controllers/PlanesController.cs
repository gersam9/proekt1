using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proekt1.Data;

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
        // GET: Plane
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plane.ToListAsync());
        }

        // GET: Plane/Details/5
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
        private bool FlightExists(int id)
        {
            return _context.Plane.Any(e => e.PlaneID == id);
        }
    }
}
