using System.Diagnostics;
using Library.Data.DataBase;
using Library.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _appDbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
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
    public IActionResult AddUserForm(string firstName, string secondName)
    {
        User user = new User();
        string FullName = $"First name - {firstName} \nSecond name - {secondName}";
        user.FirstName = firstName;
        user.SecondName = secondName;
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
}