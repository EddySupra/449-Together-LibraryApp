using LibraryManagement.Api.Models;

namespace LibraryManagement.Api.Repositories;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(Guid id);
    Book Add(Book book);
    Book Update(Book book);
    bool ExistsByIsbn(string isbn);
}
