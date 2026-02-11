# Board Game Library

An application for managing a board game library, and managing usage of said library. Intended to be used for conventions or events that want to run some form of board game library. Built with Claude.AI, ASP.NET Core MVC, and SQLite.

## Features

- **User Authentication**: Secure registration and login using ASP.NET Core Identity
- **Game Management**: Add, edit, and delete board games from your collection
- **Detailed Game Information**: Track publisher, year, player count, playing time, complexity, and descriptions
- **Loan Tracking**: Keep track of which games you've lent to friends
- **Borrowing History**: View games you've borrowed from others
- **Due Date Management**: Set and monitor due dates for borrowed games
- **Overdue Alerts**: Automatically identifies overdue games

## Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Frontend**: Bootstrap 5, Razor Views
- **Validation**: jQuery Validation

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Getting Started

### 1. Clone or Download the Project

Download the project files to your local machine.

### 2. Navigate to Project Directory

```bash
cd BoardGameLibrary
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Run the Application

```bash
dotnet run
```

The application will start and be available at:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000

### 5. First Time Setup

User's can have one of two roles: Librarian or Patron. Patrons can view the contents of the library, and see what items they are currently borrowing. 
Librarians can maintain items in the library, and can lend items to other users.

When running the first time, several demo accounts will be created. Check out DbInitializer.cs for the login info.

## Database

The application uses SQLite, which creates a file-based database (`boardgamelibrary.db`) in the project directory. This means:
- No database server installation required
- Easy to backup (just copy the .db file)
- Portable and lightweight

The database is automatically created when you first run the application.

## Project Structure

```
BoardGameLibrary/
├── Controllers/          # MVC Controllers
│   ├── AccountController.cs    # Authentication
│   ├── GamesController.cs      # Game management
│   ├── LoansController.cs      # Loan tracking
│   └── HomeController.cs       # Home page
├── Models/              # Data models and view models
│   ├── ApplicationUser.cs
│   ├── BoardGame.cs
│   ├── Loan.cs
│   └── AccountViewModels.cs
├── Views/               # Razor views
│   ├── Account/         # Login/Register views
│   ├── Games/           # Game CRUD views
│   ├── Loans/           # Loan management views
│   ├── Home/            # Home page
│   └── Shared/          # Layout and shared views
├── Data/                # Database context
│   ├── ApplicationDbContext.cs
|	└── DbInitializer.cs
└── wwwroot/             # Static files (CSS, JS)
```

## Usage Guide

### Adding Games

1. Navigate to **My Games**
2. Click **Add New Game**
3. Fill in game details (only title is required)
4. Click **Add Game**

### Lending Games

1. Go to **My Games**
2. Find the game you want to lend
3. Click **Lend**
4. Select a borrower (must be a registered user)
5. Set loan date and optional due date
6. Click **Create Loan**

### Tracking Returns

1. Navigate to **Loans**
2. Find the loan you want to mark as returned
3. Click **Mark Returned**

### Viewing Borrowed Games

1. Navigate to **My Borrowed Games**
2. See all games you've borrowed from others
3. Check due dates to know when to return them

## Security Features

- Password requirements: minimum 6 characters, uppercase, lowercase, and digit
- Anti-forgery tokens on all forms
- User isolation: users can only see and manage their own games
- Secure password hashing with ASP.NET Core Identity

## Customization

### Password Requirements

Edit `Program.cs` to modify password requirements:

```csharp
options.Password.RequireDigit = true;
options.Password.RequireLowercase = true;
options.Password.RequireUppercase = true;
options.Password.RequireNonAlphanumeric = false;
options.Password.RequiredLength = 6;
```

### Database Location

Edit `appsettings.json` to change the database file location:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=your-custom-path.db"
}
```

## Building for Production

To build the application for deployment:

```bash
dotnet publish -c Release -o ./publish
```

The published files will be in the `./publish` directory.

## Troubleshooting

### Port Already in Use

If ports 5000/5001 are already in use, you can specify different ports:

```bash
dotnet run --urls "http://localhost:5500;https://localhost:5501"
```

### Database Issues

If you encounter database issues, delete the `boardgamelibrary.db` file and restart the application. This will create a fresh database.

### Migration Issues

If you modify the models and need to update the database:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

Note: The application uses `EnsureCreated()` for simplicity, so migrations are optional for basic use.

## Future Enhancement Ideas

- Export collection to CSV/Excel
- Import games from BoardGameGeek API
- Game images/cover art
- Search and filter functionality
- Statistics dashboard
- Email notifications for overdue games
- Multiple locations/shelves
- Wishlists and expansion tracking

## License

This project is provided as-is for educational and personal use.

## Support

For issues or questions, please review the code comments and this README. The application follows standard ASP.NET Core MVC patterns.
