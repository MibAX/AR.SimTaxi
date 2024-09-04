using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AR.SimTaxi.Data;
using AR.SimTaxi.Data.Entities;
using AutoMapper;
using AR.SimTaxi.Models.Bookings;

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

            var booking = await _context
                                    .Bookings
                                    .Include(booking => booking.Car)
                                    .Include(booking => booking.Driver)
                                    .Include(booking => booking.Passengers)
                                    .Where(booking => booking.Id == id)
                                    .SingleOrDefaultAsync();

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDetailsVM = _mapper.Map<BookingDetailsViewModel>(booking);

            return View(bookingDetailsVM);
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
        public async Task<IActionResult> Edit(int id, CreateUpdateBookingViewModel createUpdateBookingVM)
        {
            if (id != createUpdateBookingVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get booking from DB including the passengers
                var booking = await _context
                                        .Bookings
                                        .Include(booking => booking.Passengers)
                                        .Where(booking => booking.Id == id)
                                        .SingleOrDefaultAsync();

                if(booking == null)
                {
                    return NotFound();
                }

                // Patch (Copy) createUpdateBookingVM into the booking
                _mapper.Map(createUpdateBookingVM, booking);

                // Update passenger list 
                await UpdateBookingPassengers(booking, createUpdateBookingVM.PassengerIds);

                // Update price
                booking.TotalPrice = GetBookingPrice();

                _context.Update(booking);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            createUpdateBookingVM.CarLookup = new SelectList(_context.Cars, "Id", "Info");
            createUpdateBookingVM.DriverLookup = new SelectList(_context.Drivers, "Id", "FullName");
            createUpdateBookingVM.PassengerLookup = new MultiSelectList(_context.Passengers, "Id", "FullName");

            return View(createUpdateBookingVM);
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
            return _context
                        .Bookings
                        .Any(booking => booking.Id == id);
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
