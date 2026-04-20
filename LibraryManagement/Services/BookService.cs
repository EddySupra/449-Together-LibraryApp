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
                TotalCopies = b.TotalCopies,
                AvailableCopies = b.AvailableCopies
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
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies
        };
    }

    public BookResponse CreateBook(CreateBookRequest request)
    {
        // AvailableCopies starts equal to TotalCopies on creation
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            TotalCopies = request.TotalCopies,
            AvailableCopies = request.TotalCopies
        };

        var created = _bookRepository.Add(book);

        return new BookResponse
        {
            Id = created.Id,
            Title = created.Title,
            Author = created.Author,
            ISBN = created.ISBN,
            TotalCopies = created.TotalCopies,
            AvailableCopies = created.AvailableCopies
        };
    }

    public BookResponse UpdateBook(Guid id, UpdateBookRequest request)
    {
        var book = _bookRepository.GetById(id)
            ?? throw new InvalidOperationException("Book not found.");

        if (request.AvailableCopies > request.TotalCopies)
            throw new InvalidOperationException("AvailableCopies cannot exceed TotalCopies.");

        book.Title = request.Title;
        book.Author = request.Author;
        book.ISBN = request.ISBN;
        book.TotalCopies = request.TotalCopies;
        book.AvailableCopies = request.AvailableCopies;

        var updated = _bookRepository.Update(book);

        return new BookResponse
        {
            Id = updated.Id,
            Title = updated.Title,
            Author = updated.Author,
            ISBN = updated.ISBN,
            TotalCopies = updated.TotalCopies,
            AvailableCopies = updated.AvailableCopies
        };
    }

    public void DeleteBook(Guid id)
    {
        var book = _bookRepository.GetById(id)
            ?? throw new InvalidOperationException("Book not found.");

        _bookRepository.Delete(book);
    }
}