using LibraryManagement.Api.Data;
using LibraryManagement.Api.Models;

namespace LibraryManagement.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books.ToList();
    }

    public Book? GetById(Guid id)
    {
        return _context.Books.FirstOrDefault(b => b.Id == id);
    }

    public Book Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        return book;
    }

    public Book Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
        return book;
    }

    public bool ExistsByIsbn(string isbn)
    {
        return _context.Books.Any(b => b.ISBN == isbn);
    }
}
