using HotelBookingSystem.Services;
using Spectre.Console;

namespace HotelBookingSystem.Management
{
    public static class MCustomers
    {
        public static void ManageCustomers()
        {
            

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("Customer Management").Centered().Color(Color.Yellow));

                var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]What would you like to do?[/]")
                        .AddChoices("Add Customer", "View Customers", "Update Customer", "Delete Customer", "Exit"));

                switch (menuChoice)
                {
                    case "Add Customer":
                        AddCustomer();
                        break;
                    case "View Customers":
                        ViewCustomers();
                        break;
                    case "Update Customer":
                        UpdateCustomer();
                        break;
                    case "Delete Customer":
                        DeleteCustomer();
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        private static void AddCustomer()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Adding a New Customer[/]\n");

            var firstName = PromptForInput("Enter First Name: ");
            var lastName = PromptForInput("Enter Last Name: ");
            var phoneNumber = PromptForValidPhoneNumber();
            var email = PromptForValidEmail();

            CustomerService.CreateCustomer(firstName, lastName, phoneNumber, email);
            AnsiConsole.Markup("[green]Customer added successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void ViewCustomers()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Viewing All Customers[/]\n");

            var customers = CustomerService.ReadCustomers();

            if (customers.Count == 0)
            {
                AnsiConsole.Markup("[red]No customers available.[/]\n");
            }
            else
            {
                var table = new Table();
                table.AddColumn("[cyan]Customer ID[/]");
                table.AddColumn("[cyan]Name[/]");
                table.AddColumn("[cyan]Phone[/]");
                table.AddColumn("[cyan]Email[/]");

                foreach (var customer in customers)
                {
                    table.AddRow(
                        customer.CustomerId.ToString(),
                        $"{customer.FirstName} {customer.LastName}",
                        customer.PhoneNumber,
                        customer.Email);
                }

                AnsiConsole.Write(table);
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void UpdateCustomer()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Updating a Customer[/]\n");

            var updateId = PromptForValidId("Enter Customer ID: ");
            var newFirstName = PromptForInput("Enter New First Name: ");
            var newLastName = PromptForInput("Enter New Last Name: ");
            var newPhoneNumber = PromptForValidPhoneNumber();
            var newEmail = PromptForValidEmail();

            CustomerService.UpdateCustomer(updateId, newFirstName, newLastName, newPhoneNumber, newEmail);
            AnsiConsole.Markup("[green]Customer updated successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void DeleteCustomer()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Deleting a Customer[/]\n");

            var deleteId = PromptForValidId("Enter Customer ID: ");

            try
            {
                CustomerService.DeleteCustomer(deleteId);
                AnsiConsole.Markup("[green]Customer deleted successfully![/]\n");
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Failed to delete customer: {ex.Message}[/]\n");
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        // Helper methods for user input
        private static string PromptForInput(string prompt)
        {
            return AnsiConsole.Ask<string>($"[cyan]{prompt}[/]");
        }

        private static int PromptForValidId(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>($"[cyan]{prompt}[/]")
                    .Validate(id => id > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]ID must be a positive number![/]")));
        }

        private static string PromptForValidPhoneNumber()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("[cyan]Enter Phone Number: [/]")
                    .Validate(phone => long.TryParse(phone, out _) ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid phone number. Try again.[/]")));
        }

        private static string PromptForValidEmail()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("[cyan]Enter Email: [/]")
                    .Validate(email => email.Contains("@") && email.Contains(".")
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid email address. Try again.[/]")));
        }
    }
}