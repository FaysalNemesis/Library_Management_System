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

        // index action --- will return all list of books
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

        // details index---- details of specific book
        public async Task<IActionResult> Details (int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book Id not provided";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with id {id}";
                    return View("NotFound");
                }
                return View(book);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book details.";
                return View("Error");
            }

            return View();  
        }
    }
}
