using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingSystem.Data;
using HotelBookingSystem.Management;
using Microsoft.EntityFrameworkCore;


namespace HotelBookingSystem.Data
{
    internal class DatabaseSeeder
    {
        public DatabaseSeeder() { }  
        
        public static void SeedData(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext), "Database context cannot be null.");
            }

            try
            {
                foreach (var room in Rooms)
                {
                    if (!dbContext.Rooms.Any(r => r.RoomId == room.RoomId))
                    {
                        dbContext.Rooms.Add(room);
                    }
                }

                
                foreach (var customer in Customers)
                {
                    if (!dbContext.Customers.Any(c => c.CustomerId == customer.CustomerId))
                    {
                        dbContext.Customers.Add(customer);
                    }
                }

                
                foreach (var booking in Bookings)
                {
                    if (!dbContext.Bookings.Any(b => b.BookingId == booking.BookingId))
                    {
                        dbContext.Bookings.Add(booking);
                    }
                }

                
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred during seeding: {ex.Message}");
            }
        }

        private static readonly List<Customer> _customers = new();
        public static IReadOnlyList<Customer> Customers => _customers.AsReadOnly();

        private static readonly List<Room> _rooms = new();
        public static IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();

        private static readonly List<Booking> _bookings = new();
        public static IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

        
        static DatabaseSeeder()
        {
            
            _rooms.AddRange(new[]
            {
                new Room { RoomId = 1, RoomType = "Single", MaxExtraBeds = 0, IsAvailable = true },
                new Room { RoomId = 2, RoomType = "Single", MaxExtraBeds = 0, IsAvailable = true },
                new Room { RoomId = 3, RoomType = "Double", MaxExtraBeds = 1, IsAvailable = true },
                new Room { RoomId = 4, RoomType = "Double", MaxExtraBeds = 2, IsAvailable = true },
            });

            
            _customers.AddRange(new[]
            {
                new Customer { CustomerId = 1, FirstName = "Alice", LastName = "Smith", PhoneNumber = "1234567890", Email = "alice@example.com" },
                new Customer { CustomerId = 2, FirstName = "Bob", LastName = "Jones", PhoneNumber = "0987654321", Email = "bob@example.com" },
                new Customer { CustomerId = 3, FirstName = "Charlie", LastName = "Brown", PhoneNumber = "5678901234", Email = "charlie@example.com" },
                new Customer { CustomerId = 4, FirstName = "Diana", LastName = "White", PhoneNumber = "4321098765", Email = "diana@example.com" },
            });

            
            _bookings.AddRange(new[]
            {
                new Booking { BookingId = 1, RoomId = 1, CustomerId = 1, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2) },
                new Booking { BookingId = 2, RoomId = 2, CustomerId = 2, CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(3) }
            });
        }

      
        public static void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            _customers.Add(customer);
        }

        public static void RemoveCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            _customers.Remove(customer);
        }

        public static void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            _rooms.Add(room);
        }

        public static void RemoveRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            _rooms.Remove(room);
        }

        public static void AddBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");
            _bookings.Add(booking);
        }

        public static void RemoveBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");
            _bookings.Remove(booking);
        }
    }
}




