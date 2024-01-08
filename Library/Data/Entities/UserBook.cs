using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities;

public class UserBook
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }
   
}