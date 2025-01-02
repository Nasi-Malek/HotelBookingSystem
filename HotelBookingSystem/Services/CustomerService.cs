using HotelBookingSystem.Data;

namespace HotelBookingSystem.Services
{
    public static class CustomerService
    {
        public static void CreateCustomer(string firstName, string lastName, string phoneNumber, string email)
        {
            int newId = DatabaseSeeder.Customers.Any() ? DatabaseSeeder.Customers.Max(c => c.CustomerId) + 1 : 1;
            DatabaseSeeder.AddCustomer(new Customer { CustomerId = newId, FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, Email = email });
        }

        public static List<Customer> ReadCustomers() => DatabaseSeeder.Customers.ToList();

        public static void UpdateCustomer(int id, string firstName, string lastName, string phoneNumber, string email)
        {
            var customer = DatabaseSeeder.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.PhoneNumber = phoneNumber;
                customer.Email = email;
            }
        }

        public static void DeleteCustomer(int id)
        {
            var customer = DatabaseSeeder.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                DatabaseSeeder.RemoveCustomer(customer);
            }
        }
    }

}