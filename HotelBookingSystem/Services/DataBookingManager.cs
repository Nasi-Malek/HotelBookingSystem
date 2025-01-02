using HotelBookingSystem.Data;
using HotelBookingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelBookingSystem.Services
{
    public class DataBookingManager
    {
        public void ShowAvailableRooms()
        {
            using (var context = new ApplicationDbContext())
            {
                var availableAndSortedRooms = context.Rooms
                                                     .Where(r => r.IsAvailable)
                                                     .OrderBy(r => r.RoomType)
                                                     .ToList();

                Console.WriteLine("\nTillgängliga och sorterade rum:");
                foreach (var room in availableAndSortedRooms)
                {
                    Console.WriteLine($"Room ID: {room.RoomId}, Type: {room.RoomType}, Available: {room.IsAvailable}");
                }
            }
        }

        public void ShowCustomerBookings(int customerId)
        {
            using (var context = new ApplicationDbContext())
            {
                var customerBookings = context.Bookings
                                              .Where(b => b.CustomerId == customerId)
                                              .OrderBy(b => b.CheckInDate)
                                              .ToList();

                Console.WriteLine($"Bokningar för kund med ID {customerId}:");
                foreach (var booking in customerBookings)
                {
                    Console.WriteLine($"Booking ID: {booking.BookingId}, Room ID: {booking.RoomId}, Check-In: {booking.CheckInDate}, Check-Out: {booking.CheckOutDate}");
                }
            }
        }

        public void AddRoom(string roomType, bool isAvailable, int maxExtraBeds = 0)
        {
            using (var context = new ApplicationDbContext())
            {
                var room = new Room
                {
                    RoomType = roomType,
                    IsAvailable = isAvailable,
                    MaxExtraBeds = maxExtraBeds
                };

                context.Rooms.Add(room);
                context.SaveChanges();
                Console.WriteLine("Room added successfully.");
            }
        }

        public void UpdateRoom(int roomId, string newRoomType, bool isAvailable, int maxExtraBeds)
        {
            using (var context = new ApplicationDbContext())
            {
                var room = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                if (room == null)
                {
                    Console.WriteLine("Room not found.");
                    return;
                }

                room.RoomType = newRoomType;
                room.IsAvailable = isAvailable;
                room.MaxExtraBeds = maxExtraBeds;

                context.SaveChanges();
                Console.WriteLine("Room updated successfully.");
            }
        }

        public void AddBooking(int customerId, int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            using (var context = new ApplicationDbContext())
            {
                var booking = new Booking
                {
                    CustomerId = customerId,
                    RoomId = roomId,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
                };

                context.Bookings.Add(booking);
                context.SaveChanges();
                Console.WriteLine("Booking added successfully.");
            }
        }

        public void UpdateBooking(int bookingId, DateTime newCheckInDate, DateTime newCheckOutDate)
        {
            using (var context = new ApplicationDbContext())
            {
                var booking = context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
                if (booking == null)
                {
                    Console.WriteLine("Booking not found.");
                    return;
                }

                booking.CheckInDate = newCheckInDate;
                booking.CheckOutDate = newCheckOutDate;

                context.SaveChanges();
                Console.WriteLine("Booking updated successfully.");
            }
        }
    }
}

