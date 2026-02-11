using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoardGameLibrary.Data;
using BoardGameLibrary.Models;

namespace BoardGameLibrary.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GamesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var games = await _context.BoardGames
                .Include(g => g.Loans)
                .OrderBy(g => g.Title)
                .ToListAsync();
            return View(games);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var boardGame = await _context.BoardGames
                .Include(g => g.Loans)
                .ThenInclude(l => l.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
	[Authorize(Roles = RoleConstants.Librarian)]
        /*        public async Task<IActionResult> Create([Bind("Title,Publisher,YearPublished,MinPlayers,MaxPlayers,PlayingTimeMinutes,Complexity,Description")] BoardGame boardGame)
                {
                    if (ModelState.IsValid)
                    {
                        boardGame.OwnerId = _userManager.GetUserId(User)!;
                        boardGame.DateAdded = DateTime.UtcNow;
                        _context.Add(boardGame);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(boardGame);
                } */
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map from ViewModel to Entity
                var boardGame = new BoardGame
                {
                    Title = model.Title,
                    Publisher = model.Publisher,
                    YearPublished = model.YearPublished,
                    MinPlayers = model.MinPlayers,
                    MaxPlayers = model.MaxPlayers,
                    PlayingTimeMinutes = model.PlayingTimeMinutes,
                    Complexity = model.Complexity,
                    Description = model.Description,
                    DateAdded = DateTime.UtcNow
                };

                _context.Add(boardGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(g => g.Id == id);

            if (boardGame == null)
            {
                return NotFound();
            }

            // Map from Entity to ViewModel
            var viewModel = new EditGameViewModel
            {
                Id = boardGame.Id,
                Title = boardGame.Title,
                Publisher = boardGame.Publisher,
                YearPublished = boardGame.YearPublished,
                MinPlayers = boardGame.MinPlayers,
                MaxPlayers = boardGame.MaxPlayers,
                PlayingTimeMinutes = boardGame.PlayingTimeMinutes,
                Complexity = boardGame.Complexity,
                Description = boardGame.Description
            };

            return View(viewModel);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
	[Authorize(Roles = RoleConstants.Librarian)]
        public async Task<IActionResult> Edit(int id, EditGameViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var existingGame = await _context.BoardGames
                .FirstOrDefaultAsync(g => g.Id == id);

            if (existingGame == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the properties from the ViewModel
                    existingGame.Title = model.Title;
                    existingGame.Publisher = model.Publisher;
                    existingGame.YearPublished = model.YearPublished;
                    existingGame.MinPlayers = model.MinPlayers;
                    existingGame.MaxPlayers = model.MaxPlayers;
                    existingGame.PlayingTimeMinutes = model.PlayingTimeMinutes;
                    existingGame.Complexity = model.Complexity;
                    existingGame.Description = model.Description;

                    // OwnerId and DateAdded are NOT changed!

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(m => m.Id == id);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
	[Authorize(Roles = RoleConstants.Librarian)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(g => g.Id == id);

            if (boardGame != null)
            {
                _context.BoardGames.Remove(boardGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BoardGameExists(int id)
        {
            return _context.BoardGames.Any(e => e.Id == id);
        }
    }
}
