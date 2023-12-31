﻿using CRUD_Library.Extensions;
using CRUD_Library.Helpers;
using CRUD_Library.Models;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Library.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HomeController
        public ActionResult Index(int page = 1)
        {
            var books = _context.Books.Include(b => b.BookReaders).ThenInclude(x => x.Reader).Include(x => x.Category).OrderByDescending(x => x.Id);
            var categories = _context.Categories;
            var readers = _context.Readers;

            var model = new IndexViewModel();

            int totalP = (int)Math.Ceiling(books.Count() / (double)model.LimitPage);
            books = (IOrderedQueryable<Book>)books.Skip((page - 1) * model.LimitPage).Take(model.LimitPage);

            model.Books = books;
            model.Categories = categories;
            model.Readers = readers;
            model.RecentBook = _context.Books.OrderByDescending(b => b.Id).Take(model.LimitPage);
            model.CurrentPages = page;
            model.TotalPages = totalP;

            return View(model);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            var books = _context.Books
                .Include(x => x.BookReaders).ThenInclude(x => x.Reader)
                .Include(x => x.Category)
                .FirstOrDefault(books => books.Id == id);
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.readers = new MultiSelectList(_context.Readers, "Id", "FIO");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Book book, IFormFile Image, int[] categories, int[] readers)
        {
            book.ImageUrl = await FileUploadHelper.UploadAsync(Image);
            if (book.ImageUrl != null)
            {
                TempData["status"] = "New book added!";
                book.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categories[0]);
                book.Date = DateTime.Now;

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                _context.BookReaders.AddRange(readers.Select(br => new BookReader() { BookId = book.Id, ReaderId = br }));
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(book);
        }
        // GET: HomeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var book = await _context.Books.
                Include(x => x.Category).
                Include(p => p.BookReaders).ThenInclude(x => x.Reader).
                FirstOrDefaultAsync(c => c.Id == id);

            var selectedCategory = _context.Categories.FirstOrDefault(x => x.Id == book.Category.Id);
            ViewBag.categories = new SelectList(_context.Categories, "Id", "Name", selectedCategory.Id);

            var selectedReaders = _context.BookReaders.Where(x => x.BookId == id).Select(x => x.ReaderId);
            ViewBag.readers = new MultiSelectList(_context.Readers, "Id", "FIO", selectedReaders);

            return View(book);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSave(Book book, IFormFile Image, int[] categories, int[] readers)
        {
            if (Image != null)
            {
                var path = await FileUploadHelper.UploadAsync(Image);
                book.ImageUrl = path;
            }

            book.Date = DateTime.Now;
            book.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categories[0]);

            //_context.Attach(book).State = EntityState.Modified;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            var bookWithTags = _context.Books.Include(x => x.BookReaders).FirstOrDefault(x => x.Id == book.Id);

            _context.UpdateManyToMany(
                bookWithTags.BookReaders,
                readers.Select(x => new BookReader { ReaderId = x, BookId = book.Id }),
                x => x.ReaderId
                );
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            return View(book);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteDelete(int id)
        {

            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            TempData["status"] = "Book DELETED!";
            return RedirectToAction("Index");
        }
    }
}
