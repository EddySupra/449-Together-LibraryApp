using LibraryManagement.Api.Dtos;

namespace LibraryManagement.Api.Services;

public interface IBookService
{
    IEnumerable<BookResponse> GetBooks();
    BookResponse? GetBookById(Guid id);
    BookResponse CreateBook(CreateBookRequest request);
}
