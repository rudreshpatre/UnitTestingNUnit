using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class BookingHelperTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _booking;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            Booking _booking;
        }

        [Test]
        public void OverlappingBookingsExist_BookingStatuCancelled_ReturnsEmptyString()
        {
            _booking = new Booking
            {
                Id = 1,
                Status = "Cancelled"
            };
            _bookingRepository.Setup(br => br.GetActiveBookings(_booking))
                              .Returns(new List<Booking>
                                { new Booking { Id = 23, Status="Yolo" } }
                              .AsQueryable);

            var result = BookingHelper.OverlappingBookingsExist(_booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_OverlappingBookingExists_ReturnsReference()
        {
            _booking = new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2021, 1, 15, 14, 0, 0),
                DepartureDate = new DateTime(2021, 1, 20, 14, 0, 0)
            };
            _bookingRepository.Setup(br => br.GetActiveBookings(_booking))
                              .Returns(new List<Booking>
                                {
                                  new Booking
                                  {
                                   Id = 2,
                                   ArrivalDate = new DateTime(2021,1,15,14,0,0),
                                   DepartureDate = new DateTime(2021,1,20,14,0,0),
                                   Reference = "a"
                                  }
                                }
                              .AsQueryable);

            var result = BookingHelper.OverlappingBookingsExist(_booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo("a"));
        }

        [Test]
        public void OverlappingBookingsExist_NoOverlappingBooking_ReturnsEmptyString()
        {
            _booking = new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2021, 1, 10, 14, 0, 0),
                DepartureDate = new DateTime(2021, 1, 14, 14, 0, 0)
            };
            _bookingRepository.Setup(br => br.GetActiveBookings(_booking))
                              .Returns(new List<Booking>
                                {
                                  new Booking
                                  {
                                   Id = 2,
                                   ArrivalDate = new DateTime(2021,1,15,14,0,0),
                                   DepartureDate = new DateTime(2021,1,20,14,0,0),
                                   Reference = "a"
                                  }
                                }
                              .AsQueryable);

            var result = BookingHelper.OverlappingBookingsExist(_booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
