using Microsoft.AspNetCore.Identity;
using BoardGameLibrary.Models;

namespace BoardGameLibrary.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Create roles if they don't exist
            if (!await roleManager.RoleExistsAsync(RoleConstants.Librarian))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstants.Librarian));
            }

            if (!await roleManager.RoleExistsAsync(RoleConstants.Patron))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstants.Patron));
            }

            // Check if we already have data
            if (context.BoardGames.Any())
            {
                return; // Database has been seeded
            }

            // Create librarian user
            var librarian = new ApplicationUser
            {
                UserName = "librarian@convention.com",
                Email = "librarian@convention.com",
                FirstName = "Sarah",
                LastName = "Librarian",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(librarian, "Librarian123!");
            await userManager.AddToRoleAsync(librarian, RoleConstants.Librarian);

            // Create patron users
            var patron1 = new ApplicationUser
            {
                UserName = "john@example.com",
                Email = "john@example.com",
                FirstName = "John",
                LastName = "Smith",
                EmailConfirmed = true
            };

            var patron2 = new ApplicationUser
            {
                UserName = "jane@example.com",
                Email = "jane@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(patron1, "Patron123!");
            await userManager.AddToRoleAsync(patron1, RoleConstants.Patron);

            await userManager.CreateAsync(patron2, "Patron123!");
            await userManager.AddToRoleAsync(patron2, RoleConstants.Patron);

            // Create board games (no OwnerId anymore!)
            var games = new List<BoardGame>
            {
                new BoardGame
                {
                    Title = "Catan",
                    Publisher = "Catan Studio",
                    YearPublished = 1995,
                    MinPlayers = 3,
                    MaxPlayers = 4,
                    PlayingTimeMinutes = 90,
                    Complexity = "Medium-Light",
                    Description = "Players collect and trade resources to build roads, settlements and cities.",
                    DateAdded = DateTime.UtcNow,
                    Quantity = 2,
                    AvailableQuantity = 2
                },
                new BoardGame
                {
                    Title = "Ticket to Ride",
                    Publisher = "Days of Wonder",
                    YearPublished = 2004,
                    MinPlayers = 2,
                    MaxPlayers = 5,
                    PlayingTimeMinutes = 60,
                    Complexity = "Light",
                    Description = "Build train routes across the map to connect cities.",
                    DateAdded = DateTime.UtcNow,
                    Quantity = 1,
                    AvailableQuantity = 1
                },
                new BoardGame
                {
                    Title = "Pandemic",
                    Publisher = "Z-Man Games",
                    YearPublished = 2008,
                    MinPlayers = 2,
                    MaxPlayers = 4,
                    PlayingTimeMinutes = 45,
                    Complexity = "Medium",
                    Description = "A cooperative game where players work together to stop global disease outbreaks.",
                    DateAdded = DateTime.UtcNow,
                    Quantity = 1,
                    AvailableQuantity = 1
                }
            };

            context.BoardGames.AddRange(games);
            await context.SaveChangesAsync();

            // Create a sample loan (librarian checking out game to patron)
            var loan = new Loan
            {
                BoardGameId = games[0].Id, // Catan
                BorrowerId = patron1.Id,
                CheckedOutById = librarian.Id,
                LoanDate = DateTime.UtcNow.AddDays(-3),
                DueDate = DateTime.UtcNow.AddDays(4),
                Notes = "First copy checked out"
            };

            games[0].AvailableQuantity = 1; // One copy is now checked out

            context.Loans.Add(loan);
            await context.SaveChangesAsync();
        }
    }
}
