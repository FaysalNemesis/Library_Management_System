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

        // create --- add new book GET ## 
        public IActionResult Create()
        {
            return View();
        }

        // Create POST#####
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Book book)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _context.Books.AddAsync(book);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Successfully added the book: {book.Title}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the book.";
                    return View(book);
                }
            }
            return View(book);
        }

        // edit GET###
        public async Task<IActionResult> Edit (int? id)
        {
             if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for editing.";
                return View("NotFound");
            }
            try
            {
                var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(m => m.BookId == id);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for editing.";
                    return View("NotFound");
                }
                return View(book);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book for editing.";
                return View("Error");
            }
        }

        // edit POST -###
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int?id, Book book)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for updating.";
                return View("NotFound");
            }

            if(ModelState.IsValid)
            {
                try
                {
                    var existingBook = await _context.Books.FindAsync(id);
                    if (existingBook == null)
                    {
                        TempData["ErrorMessage"] = $"No book found with ID {id} for updating.";
                        return View("NotFound");
                    }
                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    existingBook.ISBN = book.ISBN;
                    existingBook.PublishedDate = book.PublishedDate;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Successfully updated the book: {book.Title}.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the book.";
                    return View("Error");
                }
            }

            return View(book);
        }

        // delete 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for deletion.";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(m => m.BookId == id);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                    return View("NotFound");
                }
                return View(book);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book for deletion.";
                return View("Error");
            }
        }

        // delete post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                    return View("NotFound");
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully deleted the book: {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the book.";
                return View("Error");
            }
        }

    }
}
