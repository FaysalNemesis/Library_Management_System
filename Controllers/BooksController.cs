using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _context.Books.Include(b => b.BorrowRecords)
                    .AsNoTracking().ToListAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the books.";
                return View("Error");
            }

        }
    }
}
