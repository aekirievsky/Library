using System.Diagnostics;
using Library.Data.DataBase;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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

    [Route("Test")]
    public IActionResult Test()
    {
        /*return BadRequest(Json("Error 404"));*/
        return StatusCode(500);
    }

    [HttpGet()]
    [Route("GetUserById")]
    public IActionResult GetUserById([FromQuery(Name = "UserID")] int UserID)
    {
        var response = from u in _appDbContext.Users
            where u.UserId == UserID
            select u;

        return response == null ? NotFound() : Ok(Json(response));
    }

    [HttpGet()]
    [Route("GetBookById")]
    public IActionResult GetBookById([FromQuery(Name = "BookID")] int BookID)
    {
        var response = from b in _appDbContext.Books
            where b.BookId == BookID
            select b;
        if (response == null)
            return StatusCode(404);

        return response == null ? NotFound() : Ok(Json(response));
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
}