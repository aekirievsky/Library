using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class User
{
    [Key] public int UserId { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }

    public List<UserBook> UserBook { get; set; } = new List<UserBook>();


    public User()
    {
    }

    public User(string firstName_p, string secondName_p)
    {
        this.FirstName = firstName_p;
        this.SecondName = secondName_p;
    }
}