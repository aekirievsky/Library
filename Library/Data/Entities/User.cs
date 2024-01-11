using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class User
{
    [Key] 
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Password { get; set; }
    public List<UserBook> UserBook { get; set; } = new List<UserBook>();

    public List<UserToken> UserTokens { get; set; }


    public User()
    {
    }

    public User(string firstName_p, string secondName_p)
    {
        this.FirstName = firstName_p;
        this.SecondName = secondName_p;
    }
}