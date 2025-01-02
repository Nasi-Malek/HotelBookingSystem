using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Data
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string RoomType { get; set; } 

        [Required]
        public int MaxExtraBeds { get; set; } 

        [Required]
        public bool IsAvailable { get; set; } = true;

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } 
    }

}
