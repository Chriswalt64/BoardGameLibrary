# Quick Start Guide

Get your Board Game Library up and running in minutes!

## Prerequisites Check

Before starting, verify you have .NET 8.0 installed:

```bash
dotnet --version
```

You should see version 8.0.x or higher. If not, download from: https://dotnet.microsoft.com/download

## Installation Steps

### Step 1: Open Terminal/Command Prompt

Navigate to the BoardGameLibrary folder:

```bash
cd path/to/BoardGameLibrary
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

This downloads all required NuGet packages.

### Step 3: Run the Application

```bash
dotnet run
```

You should see output like:
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

### Step 4: Open Your Browser

Navigate to: **https://localhost:5001**

(You may need to accept the development SSL certificate)

## First Time Use

### Create Your Account

1. Click **Register** in the top navigation
2. Fill in:
   - First Name
   - Last Name
   - Email (username)
   - Password (minimum 6 characters with uppercase, lowercase, and digit)
3. Click **Register**

You'll be automatically logged in!

### Add Your First Game

1. Click **My Games** in the navigation
2. Click **Add New Game**
3. Fill in the game details:
   - **Title** (required) - e.g., "Catan"
   - Publisher (optional) - e.g., "Catan Studio"
   - Year Published - e.g., 1995
   - Min/Max Players - e.g., 3-4
   - Playing Time - e.g., 90 minutes
   - Complexity - e.g., "Medium-Light"
   - Description (optional)
4. Click **Add Game**

### Lend a Game

First, you'll need at least one other user registered (ask a friend to register or create a test account).

1. Go to **My Games**
2. Find a game card
3. Click the **Lend** button
4. Select the borrower
5. Set loan date (defaults to today)
6. Optionally set a due date
7. Click **Create Loan**

### Track What You've Borrowed

When someone lends you a game:
1. Click **My Borrowed Games**
2. View all games you currently have borrowed
3. See due dates and owners

## Common Commands

### Run the application:
```bash
dotnet run
```

### Run in watch mode (auto-restart on code changes):
```bash
dotnet watch run
```

### Build the application:
```bash
dotnet build
```

### Clean build artifacts:
```bash
dotnet clean
```

## Troubleshooting

### "Port already in use" error

Run on different ports:
```bash
dotnet run --urls "http://localhost:5500;https://localhost:5501"
```

### SSL Certificate Warning

On first run, you may need to trust the development certificate:
```bash
dotnet dev-certs https --trust
```

### Can't login after registering

Make sure you're using the email address (not first/last name) as the username.

### Database issues

If something goes wrong with the database:
1. Stop the application (Ctrl+C)
2. Delete the file: `boardgamelibrary.db`
3. Run `dotnet run` again (creates fresh database)

## Next Steps

- Explore the **Games** section to manage your collection
- Use the **Loans** section to track borrowed games
- Check **My Borrowed Games** to see what you need to return
- Invite friends to register so you can lend games to them!

## Need Help?

Check the full README.md for detailed documentation, customization options, and project structure.

Happy gaming! 🎲
