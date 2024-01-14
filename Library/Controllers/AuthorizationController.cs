using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Data.DataBase;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

    [HttpPost]
    //[Route("AuthAdmin")]
    public async Task<IActionResult> AuthAdmin([FromForm] LoginRequestViewModel.Introduction loginIntroduction)
    {
        var admin = _appDbContext.AdminUsers
            .FirstOrDefault(a => a.UserName == loginIntroduction.UserName && a.Password == loginIntroduction.Password);

        if (admin != null)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.UserName) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Admin");
        }
        else
        {
            return BadRequest("Invalid username or password");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Authorization");
    }
}