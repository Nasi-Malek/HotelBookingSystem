using HotelBookingSystem.Data;
using HotelBookingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{
    public static class RoomService
    {
        public static void CreateRoom(string roomType, int maxExtraBeds, bool isAvailable)
        {
            int newId = DatabaseSeeder.Rooms.Any() ? DatabaseSeeder.Rooms.Max(r => r.RoomId) + 1 : 1;
            DatabaseSeeder.AddRoom(new Room { RoomId = newId, RoomType = roomType, MaxExtraBeds = maxExtraBeds, IsAvailable = isAvailable });
        }

        public static List<Room> ReadRooms() => DatabaseSeeder.Rooms.ToList();

        public static void UpdateRoom(int id, string roomType, int maxExtraBeds, bool isAvailable)
        {
            var room = DatabaseSeeder.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room != null)
            {
                room.RoomType = roomType;
                room.MaxExtraBeds = maxExtraBeds;
                room.IsAvailable = isAvailable;
            }
        }

        public static void DeleteRoom(int id)
        {
            var room = DatabaseSeeder.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room != null)
            {
                DatabaseSeeder.RemoveRoom(room);
            }
        }
    }
}

