using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AR.SimTaxi.Data;
using AR.SimTaxi.Data.Entities;
using AutoMapper;
using AR.SimTaxi.Models.Passengers;

namespace AR.SimTaxi.Controllers
{
    public class PassengersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PassengersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var passengers = await _context
                                    .Passengers
                                    .ToListAsync();

            var passengerVMs = _mapper.Map<List<PassengerViewModel>>(passengers);

            return View(passengerVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context
                                    .Passengers

                                    .Include(passenger => passenger.Bookings)
                                        .ThenInclude(booking => booking.Car)

                                    .Include(passenger => passenger.Bookings)
                                        .ThenInclude(booking => booking.Driver)

                                    .Where(passenger => passenger.Id == id)
                                    .SingleOrDefaultAsync();

            if (passenger == null)
            {
                return NotFound();
            }

            var passengerDetailsVM = _mapper.Map<PassengerDetailsViewModel>(passenger);

            return View(passengerDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdatePassengerViewModel createUpdatePassengerVM)
        {
            if (ModelState.IsValid)
            {
                var passenger = _mapper.Map<Passenger>(createUpdatePassengerVM);

                _context.Add(passenger);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdatePassengerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context
                                    .Passengers
                                    .Where(passenger => passenger.Id == id)
                                    .SingleOrDefaultAsync();

            if (passenger == null)
            {
                return NotFound();
            }

            var createUpdatePassengerVM = _mapper.Map<CreateUpdatePassengerViewModel>(passenger);

            return View(createUpdatePassengerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdatePassengerViewModel createUpdatePassengerVM)
        {
            if (id != createUpdatePassengerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TO DO 
                // Get passenger from DB 
                var passenger = await  _context
                                            .Passengers
                                            .FindAsync(id);

                // Check for null reference
                if(passenger == null)
                {
                    return NotFound();
                }

                // Patch (copy) createUpdatePassengerVM into passenger
                _mapper.Map(createUpdatePassengerVM, passenger);

                _context.Update(passenger);
                await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));
            }

            return View(createUpdatePassengerVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods
        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.Id == id);
        } 

        #endregion
    }
}
