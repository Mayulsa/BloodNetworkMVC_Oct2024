using BloodNetworkMVC_Oct2024.Data;
using BloodNetworkMVC_Oct2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Controllers
{
    public class DrawersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrawersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var drawers = await _context.Drawers
                .Include(d => d.Freezer)
                .ToListAsync();
            return View(drawers);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Freezers = new SelectList(await _context.Freezers.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drawer drawer)
        {
            if (!ModelState.IsValid)
            {
                drawer.BloodUnits = new List<BloodUnit>(); // Initialize empty drawer
                _context.Add(drawer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Drawer created successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Freezers = new SelectList(await _context.Freezers.ToListAsync(), "Id", "Name", drawer.FreezerId);
            return View(drawer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drawer = await _context.Drawers.FindAsync(id);
            if (drawer == null)
            {
                return NotFound();
            }

            ViewBag.Freezers = new SelectList(await _context.Freezers.ToListAsync(), "Id", "Name", drawer.FreezerId);
            return View(drawer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Drawer drawer)
        {
            if (id != drawer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDrawer = await _context.Drawers.FindAsync(id);
                    if (existingDrawer == null)
                    {
                        return NotFound();
                    }

                    // Preserve the current count when updating
                    //drawer.CurrentCount = existingDrawer.CurrentCount;

                    _context.Entry(existingDrawer).State = EntityState.Detached;
                    _context.Update(drawer);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Drawer updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrawerExists(drawer.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            ViewBag.Freezers = new SelectList(await _context.Freezers.ToListAsync(), "Id", "Name", drawer.FreezerId);
            return View(drawer);
        }

        private bool DrawerExists(int id)
        {
            return _context.Drawers.Any(e => e.Id == id);
        }
    }
}
