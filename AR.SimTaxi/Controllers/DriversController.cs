using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AR.SimTaxi.Data;
using AR.SimTaxi.Data.Entities;
using AutoMapper;
using AR.SimTaxi.Models.Drivers;

namespace AR.SimTaxi.Controllers
{
    public class DriversController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DriversController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drivers = await _context
                                    .Drivers
                                    .OrderBy(driver => driver.FirstName)
                                    .ToListAsync();

            var driverVMs = _mapper.Map<List<Driver>, List<DriverViewModel>>(drivers);

            return View(driverVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context
                                    .Drivers
                                    .Include(driver => driver.Cars)
                                    .Where(driver => driver.Id == id)
                                    .SingleOrDefaultAsync();

            if (driver == null)
            {
                return NotFound();
            }

            var driverDetailsVM = _mapper.Map<Driver, DriverDetailsViewModel>(driver);

            return View(driverDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateDriverViewModel createUpdateDriverVM)
        {
            if (ModelState.IsValid)
            {
                var driver = _mapper.Map<CreateUpdateDriverViewModel, Driver>(createUpdateDriverVM);

                _context.Add(driver);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateDriverVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context
                                    .Drivers
                                    .FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            var driverVM = _mapper.Map<Driver, CreateUpdateDriverViewModel>(driver);

            return View(driverVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateDriverViewModel createUpdateDriverVM)
        {
            if (id != createUpdateDriverVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO
                // Get the driver from the DB
                var driver = await _context
                                    .Drivers
                                    .Where(driver => driver.Id == id)
                                    .SingleOrDefaultAsync();

                if (driver == null)
                {
                    return NotFound();
                }

                // Patch (copy) the value from createUpdateDriverVM to driver
                _mapper.Map(createUpdateDriverVM, driver);

                _context.Update(driver);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateDriverVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }
            else
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }

        #endregion
    }
}
