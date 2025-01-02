using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; } 

        [Required]
        public string LastName { get; set; } 

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value) || !new EmailAddressAttribute().IsValid(value))
                {
                    throw new ArgumentException("Invalid email format.");
                }
                _email = value;
            }
        }
        private string _email;

        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } 
    }

}
