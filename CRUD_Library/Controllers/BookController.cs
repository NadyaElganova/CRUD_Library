using CRUD_Library.Helpers;
using CRUD_Library.Models;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var categories = _context.Categories;
            var readers = _context.Readers;

            var model = new IndexViewModel();
            model.Books = books;
            model.Categories = categories;
            model.Readers = readers;
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.categories = blogDbContext.Categories;

            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.readers = new MultiSelectList(_context.Readers, "Id", "FIO");
            return View();
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book book, IFormFile Image, int[] categories, int[] readers)
        {
            // if(ModelState.IsValid)
            // {
            book.ImageUrl = await FileUploadHelper.UploadAsync(Image);
            if (book.ImageUrl != null)
            {
                TempData["status"] = "New book added!";
                book.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categories[0]);
                book.Date = DateTime.Now;      
               
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                _context.BookReaders.AddRange(readers.Select(br =>  new BookReader()  {BookId = book.Id, ReaderId = br }));
                await _context.SaveChangesAsync();

                //var readersCollection = _context.BookReaders;
                //for (int i = 0; i < readers.Count(); i++)
                //{
                //    var bookReader = new BookReader() { BookId = book.Id, ReaderId = readers[i] };
                //    readersCollection.Add(bookReader);
                //    await _context.SaveChangesAsync();
                //}

                return RedirectToAction("Index");
            }

            // }
            return View(book);
        }


    }
}
