using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using MVCBook.Data;
using MVCBook.Interfaces;
using MVCBook.Models;
using MVCBook.ViewModels;

namespace MVCBook.Controllers
{
    public class BooksController : Controller
    {
        private readonly MVCBookContext _context;
        private readonly IBufferedFileUploadService _bufferedFileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBufferedFileUploadService1 _bufferedFileUploadService1;
        private readonly IWebHostEnvironment _webHostEnvironment1;
        public BooksController(MVCBookContext context, IBufferedFileUploadService bufferedFileUploadService, IWebHostEnvironment webHostEnvironment, IBufferedFileUploadService1 bufferedFileUploadService1, IWebHostEnvironment webHostEnvironment1)
        {
            _context = context;
            _bufferedFileUploadService = bufferedFileUploadService;
            _webHostEnvironment = webHostEnvironment;
            _bufferedFileUploadService1 = bufferedFileUploadService1;
            _webHostEnvironment1 = webHostEnvironment1;
        }

        // GET: Books

        
        /*
        public async Task<IActionResult> Index()
        {
            var mVCBookContext = _context.Book.Include(b => b.Author);
            return View(await mVCBookContext.ToListAsync());
        }
        */

        
        public async Task<IActionResult> Index(string searchString, int? id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MvcBookContext.Movie'  is null.");
            }

            var books = from b in _context.Book
                         select b;
            books = books.Include(p => p.Reviews);

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title!.Contains(searchString));
            }

            if(id<1||id==null)
            {
                books = books.Include(m => m.Author);
            }
            else
            {
                books = books.Include(m => m.Author).Where(b => b.AuthorId == id);
            }
            return View(await books.ToListAsync());
        }
        
        public async Task<IActionResult> SearchByGenre(int? id)
        {
            IQueryable<BookGenre> bookgenres = _context.BookGenre.AsQueryable();
            IQueryable<Book> b=_context.Book.AsQueryable();
            if(id==null||id<1)
            {
                bookgenres = bookgenres.Include(m => m.Book).ThenInclude(m => m.Author);
            }
            else
            {
                bookgenres=bookgenres.Include(m=>m.Book).ThenInclude(m=>m.Author).Include(m=>m.Genre).Where(m=>m.GenreId==id);
            }
            b=bookgenres.Select(p=>p.Book);
            return View("~/Views/Books/Index.cshtml", await b.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b=>b.Reviews)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName");
            ViewData["Genres"] = new MultiSelectList(_context.Set<Genre>(), "Id", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,YearPublished,NumPages,Description,Publisher,FrontPage,DownloadUrl,AuthorId")] Book book, IFormFile? file, IFormFile? file1)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string slika_pateka = await _bufferedFileUploadService.UploadFile(file, _webHostEnvironment);
                    if (slika_pateka != "none")
                    {
                        ViewBag.Message = "File Upload Successful!";
                    }
                    else
                    {
                        ViewBag.Message = "File Upload Failed!";
                    }
                    book.FrontPage = slika_pateka;
                }

                if(file1 != null)
                {
                    string url_pateka = await _bufferedFileUploadService1.UploadFile1(file1, _webHostEnvironment1);
                    if (url_pateka != "none")
                    {
                        ViewBag.Message = "File Upload Successful!";
                    }
                    else
                    {
                        ViewBag.Message = "File Upload Failed!";
                    }
                    book.DownloadUrl = url_pateka;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", book.AuthorId);
            return View(book);
        }

        public async Task<IActionResult> DownloadFile(string Url)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/url/"+Url);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", "Book pdf version.pdf");
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = _context.Book.Where(m => m.Id == id).Include(m => m.Genres).First();
            /*var book = await _context.Book.FindAsync(id);*/

            if (book == null)
            {
                return NotFound();
            }

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.GenreName);
            BookGenresEditViewModel viewmodel = new BookGenresEditViewModel
            {
                Book = book,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = book.Genres.Select(sa => sa.GenreId)
            };

            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", book.AuthorId);
            return View(viewmodel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookGenresEditViewModel viewmodel, IFormFile? file, IFormFile? file1)
        {
            if (id != viewmodel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Book);
                    await _context.SaveChangesAsync();
                    IEnumerable<int> newGenreList = viewmodel.SelectedGenres;
                    IEnumerable<int> prevGenreList = _context.BookGenre.Where(s => s.BookId == id).Select(s => s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s => s.BookId == id);
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newGenreList.Contains(s.GenreId));
                        foreach (int genreId in newGenreList)
                        {
                            if (!prevGenreList.Any(s => s == genreId))
                            {
                                _context.BookGenre.Add(new BookGenre { GenreId = genreId, BookId = id });
                            }
                        }
                    }
                    _context.BookGenre.RemoveRange(toBeRemoved);
                    if(file != null)
                    {
                        string slika_pateka = await _bufferedFileUploadService.UploadFile(file, _webHostEnvironment);
                        if (slika_pateka != "none")
                        {
                            ViewBag.Message = "File Upload Successful!";
                        }
                        else
                        {
                            ViewBag.Message = "File Upload Failed!";
                        }
                        viewmodel.Book.FrontPage = slika_pateka;
                        _context.Update(viewmodel.Book);
                    }

                    if(file1 != null)
                    {
                        string url_pateka = await _bufferedFileUploadService1.UploadFile1(file1, _webHostEnvironment1);
                        if (url_pateka != "none")
                        {
                            ViewBag.Message = "File Upload Successful!";
                        }
                        else
                        {
                            ViewBag.Message = "File Upload Failed!";
                        }
                        viewmodel.Book.DownloadUrl = url_pateka;
                        _context.Update(viewmodel.Book);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewmodel.Book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", viewmodel.Book.AuthorId);
            return View(viewmodel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'MVCBookContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
