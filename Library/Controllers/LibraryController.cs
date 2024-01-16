using Library.Data.DataBase;
using Library.Data.Entities;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class LibraryController : Controller
{
    private readonly AppDbContext _appDbContext;

    public LibraryController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpGet]
    public IActionResult CheckBooksList()
    {
        var books = new List<BooksViewModel>();

        foreach (var book in _appDbContext.Books)
        {
            books.Add(new BooksViewModel()
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear
            });
        }

        var allBooksViewModel = new AllBooksViewModel()
        {
            Books = books
        };

        return View("ViewBooksList",allBooksViewModel);
    }
}