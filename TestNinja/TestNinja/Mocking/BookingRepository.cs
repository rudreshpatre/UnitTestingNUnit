using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(Booking booking);
    }

    class BookingRepository : IBookingRepository
    {
        private UnitOfWork _unitOfWork;
        public BookingRepository()
        {
            _unitOfWork = new UnitOfWork();
        }
        public IQueryable<Booking> GetActiveBookings(Booking booking)
        {
            var bookings = _unitOfWork.Query<Booking>()
                   .Where(
                       b => b.Id != booking.Id && b.Status != "Cancelled");
            return bookings;
        }
    }
}
