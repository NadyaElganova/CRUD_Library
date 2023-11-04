using CRUD_Library.Helpers;
using CRUD_Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Library.Controllers
{
    public class BookController: Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _context.Books.Include(b=>b.Category).ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _context.Categories;
            return View(categories);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book book, IFormFile Image, int categoryId)
        {
            // if(ModelState.IsValid)
            // {
            book.ImageUrl = await FileUploadHelper.UploadAsync(Image);
            if(book.ImageUrl != null)
            {
                TempData["status"] = "New book added!";
                book.Category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id==categoryId);
                book.Date = DateTime.Now;
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // }
            return View(book);
        }


    }
}
