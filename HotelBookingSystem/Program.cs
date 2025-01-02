using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HotelBookingSystem.Data;
using HotelBookingSystem.Management;
using System.Drawing;

namespace HotelBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var Dbcontext = new ApplicationDbContext())
            {
                try
                {
                    DatabaseSeeder.SeedData(Dbcontext);
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error seeding data:[/] {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        AnsiConsole.MarkupLine($"[red]Inner Exception:[/] {ex.InnerException.Message}");
                    }
                }

                var builder = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.json", true, true);
                var config = builder.Build();

                var options = new DbContextOptionsBuilder<ApplicationDbContext>();
                var connectionString = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);

                using (var dbContext = new ApplicationDbContext(options.Options))
                {
                    dbContext.Database.Migrate();
                }

                while (true)
                {
                    AnsiConsole.Clear();
                    AnsiConsole.Write(
                        new FigletText("Hotel Management")
                            .Centered()
                            .Color(Spectre.Console.Color.Aqua));

                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Choose an option:[/]")
                            .PageSize(4)
                            .AddChoices("Manage Customers", "Manage Rooms", "Manage Bookings", "Exit"));

                    switch (choice)
                    {
                        case "Manage Customers":
                            MCustomers.ManageCustomers();
                            break;
                        case "Manage Rooms":
                            MRooms.ManageRooms();
                            break;
                        case "Manage Bookings":
                            MBookings.ManageBookings();
                            break;
                        case "Exit":
                            AnsiConsole.MarkupLine("[green]Exiting the application. Goodbye![/]");
                            return;
                    }
                }
            }
        }
    }
}
