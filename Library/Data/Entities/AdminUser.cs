using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class AdminUser
{
    public int AdminId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
}