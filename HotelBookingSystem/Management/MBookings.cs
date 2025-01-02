using Spectre.Console;
using HotelBookingSystem.Services;
using HotelBookingSystem.Data;
using HotelBookingSystem.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Management
{
    public static class MBookings
    {
        public static void ManageBookings()
        {
            

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("Booking Management").Centered().Color(Color.Yellow));

                var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]What would you like to do?[/]")
                        .AddChoices("Add Booking", "View Bookings", "Update Booking", "Delete Booking", "Exit"));

                switch (menuChoice)
                {
                    case "Add Booking":
                        AddBooking();
                        break;
                    case "View Bookings":
                        ViewBookings();
                        break;
                    case "Update Booking":
                        UpdateBooking();
                        break;
                    case "Delete Booking":
                        DeleteBooking();
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        private static void AddBooking()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Adding a New Booking[/]\n");

            var roomId = PromptForValidId("Enter Room Id: ");
            var customerId = PromptForValidId("Enter Customer Id: ");
            var checkInDate = PromptForValidDate("Enter Check-In Date (yyyy-mm-dd): ", DateTime.Today, "The date must be today or in the future.");
            var checkOutDate = PromptForValidDate($"Enter Check-Out Date (yyyy-mm-dd): ", checkInDate.AddDays(1), "The date must be after the Check-In Date.");

            BookingService.CreateBooking(roomId, customerId, checkInDate, checkOutDate);
            AnsiConsole.Markup("[green]Booking added successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void ViewBookings()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Viewing All Bookings[/]\n");

            var bookings = BookingService.ReadBookings();

            if (bookings.Count == 0)
            {
                AnsiConsole.Markup("[red]No bookings available.[/]\n");
            }
            else
            {
                var table = new Table();
                table.AddColumn("[cyan]Booking ID[/]");
                table.AddColumn("[cyan]Room ID[/]");
                table.AddColumn("[cyan]Customer ID[/]");
                table.AddColumn("[cyan]Check-In[/]");
                table.AddColumn("[cyan]Check-Out[/]");

                foreach (var booking in bookings)
                {
                    table.AddRow(
                        booking.BookingId.ToString(),
                        booking.RoomId.ToString(),
                        booking.CustomerId.ToString(),
                        booking.CheckInDate.ToString("yyyy-MM-dd"),
                        booking.CheckOutDate.ToString("yyyy-MM-dd"));
                }

                AnsiConsole.Write(table);
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void UpdateBooking()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Updating a Booking[/]\n");

            var updateId = PromptForValidId("Enter Booking Id: ");
            var newRoomId = PromptForValidId("Enter New Room Id: ");
            var newCustomerId = PromptForValidId("Enter New Customer Id: ");
            var newCheckInDate = PromptForValidDate("Enter New Check-In Date (yyyy-mm-dd): ", DateTime.Today, "The date must be today or in the future.");
            var newCheckOutDate = PromptForValidDate($"Enter New Check-Out Date (yyyy-mm-dd): ", newCheckInDate.AddDays(1), "The date must be after the Check-In Date.");

            BookingService.UpdateBooking(updateId, newRoomId, newCustomerId, newCheckInDate, newCheckOutDate);
            AnsiConsole.Markup("[green]Booking updated successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        private static void DeleteBooking()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Deleting a Booking[/]\n");

            var deleteId = PromptForValidId("Enter Booking Id: ");

            try
            {
                BookingService.DeleteBooking(deleteId);
                AnsiConsole.Markup("[green]Booking deleted successfully![/]\n");
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Failed to delete booking: {ex.Message}[/]\n");
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        // Helper methods for user input
        private static int PromptForValidId(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>($"[cyan]{prompt}[/]")
                    .Validate(id => id > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]ID must be a positive number![/]")));
        }

        private static DateTime PromptForValidDate(string prompt, DateTime minDate, string errorMessage)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<DateTime>($"[cyan]{prompt}[/]")
                    .Validate(date => date >= minDate ? ValidationResult.Success() : ValidationResult.Error($"[red]{errorMessage}[/]")));
        }
    }
}