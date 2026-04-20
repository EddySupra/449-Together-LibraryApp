using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.Repositories;

namespace LibraryManagement.Api.Services;

public class BorrowRecordService : IBorrowRecordService
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;

    public BorrowRecordService(
        IBorrowRepository borrowRepository,
        IBookRepository bookRepository,
        IMemberRepository memberRepository)
    {
        _borrowRepository = borrowRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public IEnumerable<BorrowRecordResponse> GetBorrowRecords()
    {
        return _borrowRepository.GetAll().Select(MapToResponse);
    }

    public IEnumerable<BorrowRecordResponse> GetBorrowRecordsByMemberId(Guid memberId)
    {
        return _borrowRepository.GetByMemberId(memberId).Select(MapToResponse);
    }

    public BorrowRecordResponse BorrowBook(CreateBorrowRequest request)
    {
        var book = _bookRepository.GetById(request.BookId)
            ?? throw new InvalidOperationException("Book not found.");

        var member = _memberRepository.GetById(request.MemberId)
            ?? throw new InvalidOperationException("Member not found.");

        if (book.CopiesAvailable <= 0)
            throw new InvalidOperationException("No copies of this book are available.");

        if (_borrowRepository.HasActiveBorrow(request.MemberId, request.BookId))
            throw new InvalidOperationException("Member already has an active borrow for this book.");

        // Decrement available copies
        book.CopiesAvailable--;
        _bookRepository.Update(book);

        var record = new BorrowRecord
        {
            Id = Guid.NewGuid(),
            MemberId = request.MemberId,
            BookId = request.BookId,
            BorrowedAt = DateTime.UtcNow,
            DueAt = DateTime.UtcNow.AddDays(14),
            Status = "Borrowed",
            Member = member,
            Book = book
        };

        var created = _borrowRepository.Add(record);
        return MapToResponse(created);
    }

    public BorrowRecordResponse ReturnBook(Guid borrowRecordId)
    {
        var record = _borrowRepository.GetById(borrowRecordId)
            ?? throw new InvalidOperationException("Borrow record not found.");

        if (record.Status != "Borrowed")
            throw new InvalidOperationException("This book has already been returned.");

        record.Status = "Returned";
        record.ReturnedAt = DateTime.UtcNow;

        // Increment available copies
        record.Book!.CopiesAvailable++;
        _bookRepository.Update(record.Book);

        var updated = _borrowRepository.Update(record);
        return MapToResponse(updated);
    }

    private static BorrowRecordResponse MapToResponse(BorrowRecord br) => new()
    {
        Id = br.Id,
        MemberId = br.MemberId,
        BookId = br.BookId,
        BookTitle = br.Book?.Title ?? string.Empty,
        MemberFullName = br.Member is not null
            ? $"{br.Member.FirstName} {br.Member.LastName}"
            : string.Empty,
        BorrowedAt = br.BorrowedAt,
        DueAt = br.DueAt,
        ReturnedAt = br.ReturnedAt,
        Status = br.Status
    };
}
