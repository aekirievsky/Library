using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.DataBase;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<UserBook> UserBook { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(t => t.UserId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Book>().Property(t => t.BookId).ValueGeneratedOnAdd();

        modelBuilder.Entity<UserBook>()
            .HasKey(x => new { x.UserId, x.BookId });

        modelBuilder.Entity<UserBook>()
            .HasOne(x => x.Book)
            .WithMany(x => x.UserBook)
            .HasForeignKey(x => x.BookId);

        modelBuilder.Entity<UserBook>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserBook)
            .HasForeignKey(x => x.UserId);
    }

    public void AddUser(User user_p)
    {
        Users.Add(user_p);
        SaveChanges();
    }

    public void AddBook(Book book_p)
    {
        Books.Add(book_p);
        SaveChanges();
    }
    
    
}