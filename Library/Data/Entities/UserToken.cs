namespace Library.Data.Entities;

public class UserToken
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public User User { get; set; }
}