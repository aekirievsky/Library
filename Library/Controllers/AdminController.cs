using System.Diagnostics;
using Library.Data.DataBase;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly AppDbContext _appDbContext;

    public AdminController(ILogger<AdminController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet()]
    public IActionResult AddUserForm()
    {
        return View("~/Views/Forms/AddUser.cshtml");
    }

    [HttpPost]
    public IActionResult AddUserForm(string userName, string password)
    {
        User user = new User()
        {
            UserName = userName,
            Password = password
        };

        try
        {
            _appDbContext.AddUser(user);
        }
        catch (Exception e)
        {
            return View("~/Views/SuccessModel.cshtml", new ResponseViewModel
            {
                Success = false
            });
        }


        return View("~/Views/SuccessModel.cshtml", new ResponseViewModel
        {
            Success = true
        });
    }

    [HttpGet()]
    public IActionResult AddBookForm()
    {
        return View("~/Views/Forms/AddBook.cshtml");
    }

    [HttpPost]
    public IActionResult AddBookForm(string title, string author, int publicationYear)
    {
        Book book = new Book()
        {
            Title = title,
            Author = author,
            PublicationYear = publicationYear
        };

        try
        {
            _appDbContext.AddBook(book);
        }
        catch (Exception e)
        {
            return View("~/Views/SuccessModel.cshtml", new ResponseViewModel
            {
                Success = false
            });
        }


        return View("~/Views/SuccessModel.cshtml", new ResponseViewModel
        {
            Success = true
        });
    }

    [HttpGet]
    public IActionResult CheckUsersList()
    {
        var users = new List<UsersViewModel>();

        foreach (var user in _appDbContext.Users)
        {
            users.Add(new UsersViewModel()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password
            });
        }

        var allUsersList = new AllUsersViewModel()
        {
            Users = users
        };

        return View("ViewUsersList", allUsersList);
    }

    [HttpGet]
    public IActionResult CheckUserBookList()
    {
        var userName = new List<string>();

        foreach (var item in _appDbContext.Users)
        {
            userName.Add(item.UserName);
        }

        return View("ViewUserBookList", new UserNameViewModel
        {
            UserNameList = userName
        });
    }

    [HttpPost]
    public IActionResult BookListByUser(string userName)
    {
        var bookList = from u in _appDbContext.Users
                .Include(u => u.UserBook)
                .ThenInclude(u=>u.Book)
            where u.UserName == userName
            select u;

        var books = new List<BooksViewModel>();

        foreach (var userBook in bookList.FirstOrDefault().UserBook)
        {
            var book = userBook.Book;
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

        return View("~/Views/Admin/ViewBooksListForAdmin.cshtml",allBooksViewModel);
    }
}