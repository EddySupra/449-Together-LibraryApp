using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.Repositories;

namespace LibraryManagement.Api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<BookResponse> GetBooks()
    {
        return _bookRepository.GetAll()
            .Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Genre = b.Genre,
                PublishedAt = b.PublishedAt,
                CopiesAvailable = b.CopiesAvailable
            });
    }

    public BookResponse? GetBookById(Guid id)
    {
        var book = _bookRepository.GetById(id);
        if (book is null)
            return null;

        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Genre = book.Genre,
            PublishedAt = book.PublishedAt,
            CopiesAvailable = book.CopiesAvailable
        };
    }

    public BookResponse CreateBook(CreateBookRequest request)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            Genre = request.Genre,
            PublishedAt = request.PublishedAt,
            CopiesAvailable = request.CopiesAvailable
        };

        var created = _bookRepository.Add(book);

        return new BookResponse
        {
            Id = created.Id,
            Title = created.Title,
            Author = created.Author,
            ISBN = created.ISBN,
            Genre = created.Genre,
            PublishedAt = created.PublishedAt,
            CopiesAvailable = created.CopiesAvailable
        };
    }
}
