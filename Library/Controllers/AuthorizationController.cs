using Library.Data.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class AuthorizationController : Controller
{
    private readonly AppDbContext _appDbContext;

    public AuthorizationController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        return View();
    }
}