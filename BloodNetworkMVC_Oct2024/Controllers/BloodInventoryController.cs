using BloodNetworkMVC_Oct2024.Data;
using BloodNetworkMVC_Oct2024.Models;
using BloodNetworkMVC_Oct2024.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Controllers
{
    public class BloodInventoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IBloodInventoryService _inventoryService;

        public BloodInventoryController(ApplicationDbContext context, IBloodInventoryService inventoryService)
        {
            _context = context;
            _inventoryService = inventoryService;
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
            if (!ModelState.IsValid)
            {
                bloodUnit.ExpirationDate = bloodUnit.CollectionDate.AddDays(42); // Blood typically expires after 42 days
                bloodUnit.Status = "Available";
                _context.Add(bloodUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodUnit);
        }

        //private readonly IBloodInventoryService _inventoryService;

        //public BloodInventoryController(IBloodInventoryService inventoryService)
        //{
        //    _inventoryService = inventoryService;
        //}

        public async Task<IActionResult> Dashboard()
        {
            var report = await _inventoryService.GenerateInventoryReport();
            var expiringUnits = await _inventoryService.GetExpiringBloodUnits(7);

            //var viewModel = new DashboardViewModel
            //{
            //    Report = report,
            //    ExpiringUnits = expiringUnits
            //};

            return View();
        }

        public async Task<IActionResult> BloodGroupInventory(string bloodGroup)
        {
            var units = await _inventoryService.FilterByBloodGroup(bloodGroup);
            return View(units);
        }

        public async Task<IActionResult> ExpiringUnits(int days = 7)
        {
            var units = await _inventoryService.GetExpiringBloodUnits(days);
            return View(units);
        }


        //private readonly IBloodInventoryService _inventoryService;

        //public BloodInventoryController(IBloodInventoryService inventoryService)
        //{
        //    _inventoryService = inventoryService;
        //}

        //public async Task<IActionResult> Index(BloodInventoryFilter filter)
        //{
        //    var bloodUnits = await _inventoryService.FilterBloodUnits(filter);
        //    var expiringUnits = await _inventoryService.GetExpiringBloodUnits(7);

        //    var viewModel = new BloodInventoryViewModel
        //    {
        //        BloodUnits = bloodUnits,
        //        ExpiringUnits = expiringUnits,
        //        Filter = filter
        //    };

        //    return View(viewModel);
        //}

        //public async Task<IActionResult> Report()
        //{
        //    var report = await _inventoryService.GenerateInventoryReport();
        //    return View(report);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ExpirationAlerts(int daysThreshold = 7)
        //{
        //    var expiringUnits = await _inventoryService.GetExpiringBloodUnits(daysThreshold);
        //    return PartialView("_ExpirationAlerts", expiringUnits);
        //}

    }
}
