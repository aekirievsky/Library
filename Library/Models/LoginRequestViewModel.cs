using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Models;

public class LoginRequestViewModel : PageModel
{
    public class Introduction
    {
        //[BindProperty] 
        public string UserName { get; set; }

        //[BindProperty] 
        public string Password { get; set; }
    }

    public  void OnPost()
    {
        Introduction introduction = new Introduction();
        introduction.UserName = Request.Form["UserName"];
        introduction.Password = Request.Form["Password"];
    }
}