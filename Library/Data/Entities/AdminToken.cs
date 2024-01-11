using System.ComponentModel.DataAnnotations;

namespace Library.Data.Entities;

public class AdminToken
{
    public int AdminId { get; set; }
    public string Token { get; set; }
    public AdminUser AdminUser { get; set; }
}