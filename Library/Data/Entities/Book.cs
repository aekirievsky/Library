using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities;

public class Book
{
    [Key] public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    
    public List<UserBook> UserBook { get; set; } = new List<UserBook>();

    public Book()
    {
    }

    public Book(string title_p, string author_p, int publicationYear_p)
    {
        this.Title = title_p;
        this.Author = author_p;
        this.PublicationYear = publicationYear_p;
    }
}