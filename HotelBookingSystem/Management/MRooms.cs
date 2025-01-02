using Spectre.Console;
using HotelBookingSystem.Services;

namespace HotelBookingSystem.Management
{
    public static class MRooms
    {
        public static void ManageRooms()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("Room Management").Centered().Color(Color.Green));

                var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]What would you like to do?[/]")
                        .AddChoices("Add Room", "View Rooms", "Update Room", "Delete Room", "Exit"));

                switch (menuChoice)
                {
                    case "Add Room":
                        AddRoom();
                        break;
                    case "View Rooms":
                        ViewRooms();
                        break;
                    case "Update Room":
                        UpdateRoom();
                        break;
                    case "Delete Room":
                        DeleteRoom();
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        public static void AddRoom()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Adding a New Room[/]\n");

            var roomType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [cyan]Room Type[/]:")
                    .AddChoices("Single", "Double"));

            int maxExtraBeds = 0;
            if (roomType == "Double")
            {
                maxExtraBeds = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter [cyan]Max Extra Beds (0-2)[/]:")
                        .Validate(value => value is >= 0 and <= 2
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Max Extra Beds must be between 0 and 2![/]")));
            }

            var isAvailable = AnsiConsole.Confirm("Is the room available?");

            RoomService.CreateRoom(roomType, maxExtraBeds, isAvailable);
            AnsiConsole.Markup("[green]Room added successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        public static void ViewRooms()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Viewing All Rooms[/]\n");

            var rooms = RoomService.ReadRooms();

            if (rooms.Count == 0)
            {
                AnsiConsole.Markup("[red]No rooms available.[/]\n");
            }
            else
            {
                var table = new Table();
                table.AddColumn("[cyan]Room ID[/]");
                table.AddColumn("[cyan]Type[/]");
                table.AddColumn("[cyan]Max Extra Beds[/]");
                table.AddColumn("[cyan]Available[/]");

                foreach (var room in rooms)
                {
                    table.AddRow(
                        room.RoomId.ToString(),
                        room.RoomType,
                        room.MaxExtraBeds.ToString(),
                        room.IsAvailable ? "[green]Yes[/]" : "[red]No[/]");
                }

                AnsiConsole.Write(table);
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        public static void UpdateRoom()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Updating a Room[/]\n");

            var updateId = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter [cyan]Room ID[/]:")
                    .Validate(id => id > 0
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Room ID must be a positive number![/]")));

            var newRoomType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select [cyan]New Room Type[/]:")
                    .AddChoices("Single", "Double"));

            int newMaxExtraBeds = 0;
            if (newRoomType == "Double")
            {
                newMaxExtraBeds = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter [cyan]New Max Extra Beds (0-2)[/]:")
                        .Validate(value => value is >= 0 and <= 2
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Max Extra Beds must be between 0 and 2![/]")));
            }

            var newIsAvailable = AnsiConsole.Confirm("Is the room available?");

            RoomService.UpdateRoom(updateId, newRoomType, newMaxExtraBeds, newIsAvailable);
            AnsiConsole.Markup("[green]Room updated successfully![/]\n");
            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }

        public static void DeleteRoom()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Deleting a Room[/]\n");

            var deleteId = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter [cyan]Room ID[/]:")
                    .Validate(id => id > 0
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Room ID must be a positive number![/]")));

            try
            {
                RoomService.DeleteRoom(deleteId);
                AnsiConsole.Markup("[green]Room deleted successfully![/]\n");
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]Failed to delete room: {ex.Message}[/]\n");
            }

            AnsiConsole.Markup("[gray]Press any key to return to the menu...[/]");
            Console.ReadKey();
        }
    }
}
