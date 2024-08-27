using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AR.SimTaxi.Data;
using AR.SimTaxi.Data.Entities;
using AR.SimTaxi.Models.Cars;
using AutoMapper;

namespace AR.SimTaxi.Controllers
{
    public class CarsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CarsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .ToListAsync();


            var carVMs = _mapper.Map<List<Car>, List<CarViewModel>>(cars);

            return View(carVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .Include(car => car.Driver)
                                .Where(car => car.Id == id)
                                .SingleOrDefaultAsync();

            if (car == null) // No car in the DB with id if found
            {
                return NotFound();
            }

            var carDetailsVM = _mapper.Map<Car, CarDetailsViewModel>(car);

            return View(carDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCarViewModel createUpdateCarVM)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<CreateUpdateCarViewModel, Car>(createUpdateCarVM);

                _context.Add(car);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateCarVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context
                                .Cars
                                .FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var createUpdateCarVM = _mapper.Map<Car, CreateUpdateCarViewModel>(car);

            return View(createUpdateCarVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCarViewModel createUpdateCarVM)
        {
            if (id != createUpdateCarVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO
                // Get the car from the DB
                var car = await  _context
                                    .Cars
                                    .Where(car => car.Id == id)
                                    .SingleOrDefaultAsync();

                if(car == null)
                {
                    return NotFound();
                }

                // Patch (Copy) the updated values from the createUpdateCarVM to the car
                _mapper.Map(createUpdateCarVM, car);


                _context.Update(car);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateCarVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context
                                .Cars
                                .FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
        #endregion
    }
}
