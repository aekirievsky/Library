using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class User
{
    [Key] public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public List<UserBook> UserBook { get; set; } = new List<UserBook>();


    public User()
    {
    }

    public User(string userName_p, string password_p)
    {
        this.UserName = userName_p;
        this.Password = password_p;
    }
}