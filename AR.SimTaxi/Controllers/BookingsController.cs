using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AR.SimTaxi.Data;
using AR.SimTaxi.Data.Entities;
using AutoMapper;
using AR.SimTaxi.Models.Bookings;
using System.Linq;

namespace AR.SimTaxi.Controllers
{
    public class BookingsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookingsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await _context
                                    .Bookings
                                    .Include(booking => booking.Car)
                                    .Include(booking => booking.Driver)
                                    .ToListAsync();

            var bookingVMs = _mapper.Map<List<BookingViewModel>>(bookings);

            return View(bookingVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateBookingVM = new CreateUpdateBookingViewModel();

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateBookingViewModel createUpdateBookingVM)
        {
            if (ModelState.IsValid)
            {
                var booking = _mapper.Map<Booking>(createUpdateBookingVM);

                booking.TotalPrice = GetBookingPrice();

                await UpdateBookingPassengers(booking, createUpdateBookingVM.PassengerIds);

                _context.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context
                                    .Bookings
                                    .Include(booking => booking.Passengers)
                                    .Where(booking => booking.Id == id)
                                    .SingleOrDefaultAsync();

            if (booking == null)
            {
                return NotFound();
            }

            var createUpdateBookingVM = _mapper.Map<CreateUpdateBookingViewModel>(booking);

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", booking.CarId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", booking.DriverId);
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        } 

        private decimal GetBookingPrice()
        {
            var random = new Random();
            var price = random.Next(10, 100);

            return price;
        }

        private async Task UpdateBookingPassengers(Booking booking, List<int> passengerIds)
        {
            // Clear Booking Passengers
            booking.Passengers.Clear();

            // Get Passengers from the DB using passengerIds
            var passengers = await _context
                                        .Passengers
                                        .Where(passenger => passengerIds.Contains(passenger.Id))
                                        .ToListAsync();

            // Add Passengers to Booking
            booking.Passengers.AddRange(passengers);
        }

        #endregion
    }
}
