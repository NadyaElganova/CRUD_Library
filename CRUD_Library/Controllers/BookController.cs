using CRUD_Library.Extensions;
using CRUD_Library.Helpers;
using CRUD_Library.Models;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace CRUD_Library.Controllers
{
    [Authorize(Roles = "User")]
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
            Book book = _context.Books
                .Include(x => x.BookReaders).ThenInclude(x => x.Reader)
                .Include(x => x.Category)
                .FirstOrDefault(books => books.Id == id);
            var detailVM = new DetailViewModel();
            detailVM.Book = book;

            var comments = _context.Comments.Include(x=>x.Book).Include(x=>x.User);
            detailVM.Comments = (IOrderedQueryable<Comment>)comments.Where(x => x.Book.Id == id);

            ClaimsPrincipal claimUser = HttpContext.User;

            // Получение номера id пользователя
            var idClaim = claimUser.FindFirst(ClaimTypes.NameIdentifier);
            var userId = idClaim != null ? idClaim.Value : null;

            // Получение имени пользователя
            var nameClaim = claimUser.FindFirst(ClaimTypes.Name);
            var userName = nameClaim != null ? nameClaim.Value : null;
            detailVM.User = _context.Users.FirstOrDefault(u=> u.Login==userName);


            return View(detailVM);
        }
        
    }
}
