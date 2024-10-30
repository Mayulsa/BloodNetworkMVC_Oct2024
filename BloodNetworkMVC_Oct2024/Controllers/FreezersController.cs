using BloodNetworkMVC_Oct2024.Data;
using BloodNetworkMVC_Oct2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Controllers
{
    public class FreezersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FreezersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var freezers = await _context.Freezers
                .Include(f => f.Drawers)
                .ToListAsync();
            return View(freezers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Freezer freezer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(freezer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Freezer created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(freezer);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freezer = await _context.Freezers
                .Include(f => f.Drawers)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (freezer == null)
            {
                return NotFound();
            }

            return View(freezer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freezer = await _context.Freezers.FindAsync(id);
            if (freezer == null)
            {
                return NotFound();
            }

            return View(freezer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Freezer freezer)
        {
            if (id != freezer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freezer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Freezer updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreezerExists(freezer.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(freezer);
        }

        private bool FreezerExists(int id)
        {
            return _context.Freezers.Any(e => e.Id == id);
        }
    }
}
