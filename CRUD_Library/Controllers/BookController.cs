using CRUD_Library.Extensions;
using CRUD_Library.Helpers;
using CRUD_Library.Models;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Configuration;

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
        public IActionResult Index(int? categoryId = null, int? readerId = null, int page=1)
        {
            var books = _context.Books.Include(b => b.BookReaders).ThenInclude(x => x.Reader).Include(x => x.Category).OrderByDescending(x => x.Id);
            var categories = _context.Categories;
            var readers = _context.Readers;

            if (categoryId != null)
            {
                books = (IOrderedQueryable<Book>)books.Where(x => x.Category.Id == categoryId);
            }
            if (readerId != null)
            {
                books = (IOrderedQueryable<Book>)books.Where(x => x.BookReaders.Any(x => x.ReaderId == readerId));
            }

            var model = new IndexViewModel();

            int totalP = (int)Math.Ceiling(books.Count() / (double)model.LimitPage);
            books = (IOrderedQueryable<Book>)books.Skip((page - 1) * model.LimitPage).Take(model.LimitPage);

            model.Books = books;
            model.Categories = categories;
            model.Readers = readers;
            model.RecentBook = _context.Books.OrderByDescending(b => b.Id).Take(model.LimitPage);
            model.CurrentPages = page;
            model.TotalPages = totalP;
            model.SelectedReaderId = readerId;
            model.SelectedCategoryId = categoryId;

            return View(model);
        }
                
        [HttpGet]
        public IActionResult Details(int id)
        {
            var books = _context.Books
                .Include(x => x.BookReaders).ThenInclude(x => x.Reader)
                .Include(x => x.Category)
                .FirstOrDefault(books => books.Id == id);
            return View(books);
        }

       
    }
}
