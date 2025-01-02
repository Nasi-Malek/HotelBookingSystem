using HotelBookingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{
    public static class BookingService
    {
        public static void CreateBooking(int roomId, int customerId, DateTime checkInDate, DateTime checkOutDate)
        {
            int newId = DatabaseSeeder.Bookings.Any() ? DatabaseSeeder.Bookings.Max(b => b.BookingId) + 1 : 1;
            DatabaseSeeder.AddBooking(new Booking { BookingId = newId, RoomId = roomId, CustomerId = customerId, CheckInDate = checkInDate, CheckOutDate = checkOutDate });
        }

        public static List<Booking> ReadBookings() => DatabaseSeeder.Bookings.ToList();

        public static void UpdateBooking(int id, int roomId, int customerId, DateTime checkInDate, DateTime checkOutDate)
        {
            var booking = DatabaseSeeder.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking != null)
            {
                booking.RoomId = roomId;
                booking.CustomerId = customerId;
                booking.CheckInDate = checkInDate;
                booking.CheckOutDate = checkOutDate;
            }
        }

        public static void DeleteBooking(int id)
        {
            var booking = DatabaseSeeder.Bookings.FirstOrDefault(b => b.BookingId == id);
            if (booking != null)
            {
                DatabaseSeeder.RemoveBooking(booking);
            }
        }
    }
}
