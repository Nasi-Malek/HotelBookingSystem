using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services
{

    public static class Validator
    {
        // Validates that the input is not null or empty
        public static bool IsNotEmpty(string input, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Input cannot be empty.";
                return false;
            }
            errorMessage = null;
            return true;
        }

       
        public static bool IsValidInteger(string input, out int result, out string errorMessage)
        {
            if (int.TryParse(input, out result))
            {
                errorMessage = null;
                return true;
            }
            errorMessage = "Invalid number. Please enter a valid integer.";
            return false;
        }

        
        public static bool IsValidPhoneNumber(string input, out string errorMessage)
        {
            if (Regex.IsMatch(input, @"^\d{10,15}$"))
            {
                errorMessage = null;
                return true;
            }
            errorMessage = "Invalid phone number. Please enter a valid number (10-15 digits).";
            return false;
        }

        
        public static bool IsValidEmail(string input, out string errorMessage)
        {
            if (Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = null;
                return true;
            }
            errorMessage = "Invalid email address. Please enter a valid email.";
            return false;
        }

       
        public static bool IsValidDate(string input, out DateTime result, out string errorMessage)
        {
            if (DateTime.TryParse(input, out result))
            {
                errorMessage = null;
                return true;
            }
            errorMessage = "Invalid date. Please enter a valid date in the correct format.";
            return false;
        }
    }
}

    
