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
                .Where(g => g.OwnerId == userId)
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
                .FirstOrDefaultAsync(m => m.Id == id && m.OwnerId == userId);

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
                    OwnerId = _userManager.GetUserId(User)!,
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
                .FirstOrDefaultAsync(g => g.Id == id && g.OwnerId == userId);

            if (boardGame == null)
            {
                return NotFound();
            }
            return View(boardGame);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Publisher,YearPublished,MinPlayers,MaxPlayers,PlayingTimeMinutes,Complexity,Description")] BoardGame boardGame)
        {
            if (id != boardGame.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var existingGame = await _context.BoardGames
                .FirstOrDefaultAsync(g => g.Id == id && g.OwnerId == userId);

            if (existingGame == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingGame.Title = boardGame.Title;
                    existingGame.Publisher = boardGame.Publisher;
                    existingGame.YearPublished = boardGame.YearPublished;
                    existingGame.MinPlayers = boardGame.MinPlayers;
                    existingGame.MaxPlayers = boardGame.MaxPlayers;
                    existingGame.PlayingTimeMinutes = boardGame.PlayingTimeMinutes;
                    existingGame.Complexity = boardGame.Complexity;
                    existingGame.Description = boardGame.Description;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardGameExists(boardGame.Id))
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
            return View(boardGame);
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
                .FirstOrDefaultAsync(m => m.Id == id && m.OwnerId == userId);

            if (boardGame == null)
            {
                return NotFound();
            }

            return View(boardGame);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var boardGame = await _context.BoardGames
                .FirstOrDefaultAsync(g => g.Id == id && g.OwnerId == userId);

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
