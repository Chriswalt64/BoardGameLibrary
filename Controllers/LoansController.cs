using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoardGameLibrary.Data;
using BoardGameLibrary.Models;

namespace BoardGameLibrary.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoansController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var loans = await _context.Loans
                .Include(l => l.BoardGame)
                .Include(l => l.Borrower)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();

            return View(loans);
        }

        // GET: Loans/MyBorrowedGames
        public async Task<IActionResult> MyBorrowedGames()
        {
            var userId = _userManager.GetUserId(User);
            var loans = await _context.Loans
                .Include(l => l.BoardGame)
                .Where(l => l.BorrowerId == userId)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();

            return View(loans);
        }

        // GET: Loans/Create
        public async Task<IActionResult> Create(int? gameId)
        {
            var userId = _userManager.GetUserId(User);
            
            ViewBag.Games = new SelectList(
                await _context.BoardGames
                    .OrderBy(g => g.Title)
                    .ToListAsync(), 
                "Id", 
                "Title",
                gameId);

            ViewBag.Borrowers = new SelectList(
                await _context.Users
                    .Where(u => u.Id != userId)
                    .OrderBy(u => u.FirstName)
                    .ToListAsync(),
                "Id",
                "FullName");

            var loan = new Loan();
            if (gameId.HasValue)
            {
                loan.BoardGameId = gameId.Value;
            }

            return View(loan);
        }

        // POST: Loans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoardGameId,BorrowerId,LoanDate,DueDate,Notes")] Loan loan)
        {
            var userId = _userManager.GetUserId(User);
            var game = await _context.BoardGames.FindAsync(loan.BoardGameId);

            if (game == null)
            {
                ModelState.AddModelError("", "Invalid game selected.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Games = new SelectList(
                await _context.BoardGames
                    .OrderBy(g => g.Title)
                    .ToListAsync(),
                "Id",
                "Title",
                loan.BoardGameId);

            ViewBag.Borrowers = new SelectList(
                await _context.Users
                    .Where(u => u.Id != userId)
                    .OrderBy(u => u.FirstName)
                    .ToListAsync(),
                "Id",
                "FullName",
                loan.BorrowerId);

            return View(loan);
        }

        // POST: Loans/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var userId = _userManager.GetUserId(User);
            var loan = await _context.Loans
                .Include(l => l.BoardGame)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (loan == null)
            {
                return NotFound();
            }

            if (!loan.IsReturned)
            {
                loan.ReturnDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var loan = await _context.Loans
                .Include(l => l.BoardGame)
                .Include(l => l.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var loan = await _context.Loans
                .Include(l => l.BoardGame)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (loan != null)
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
