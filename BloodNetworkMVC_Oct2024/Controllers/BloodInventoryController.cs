using BloodNetworkMVC_Oct2024.Data;
using BloodNetworkMVC_Oct2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Controllers
{
    public class BloodInventoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BloodInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bloodUnits = await _context.BloodUnits
                .Include(b => b.Freezer)
                .Include(b => b.Drawer)
                .ToListAsync();
            return View(bloodUnits);
        }

        public async Task<IActionResult> AddBloodUnit()
        {
            ViewBag.Freezers = new SelectList(await _context.Freezers.ToListAsync(), "Id", "Name");
            ViewBag.Drawers = new SelectList(await _context.Drawers.ToListAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBloodUnit(BloodUnit bloodUnit)
        {
            if (ModelState.IsValid)
            {
                bloodUnit.ExpirationDate = bloodUnit.CollectionDate.AddDays(42); // Blood typically expires after 42 days
                bloodUnit.Status = "Available";
                _context.Add(bloodUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodUnit);
        }

    }
}
