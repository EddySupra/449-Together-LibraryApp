using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Models;
using LibraryManagement.Api.Repositories;

namespace LibraryManagement.Api.Services;

public class BorrowRecordService : IBorrowRecordService
{
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;

    public BorrowRecordService(
        IBorrowRecordRepository borrowRecordRepository,
        IBookRepository bookRepository,
        IMemberRepository memberRepository)
    {
        _borrowRecordRepository = borrowRecordRepository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public IEnumerable<BorrowRecordResponse> GetBorrowRecords()
    {
        return _borrowRecordRepository.GetAll().Select(MapToResponse);
    }

    public IEnumerable<BorrowRecordResponse> GetBorrowRecordsByMemberId(Guid memberId)
    {
        return _borrowRecordRepository.GetByMemberId(memberId).Select(MapToResponse);
    }

    public BorrowRecordResponse BorrowBook(CreateBorrowRequest request)
    {
        var book = _bookRepository.GetById(request.BookId)
            ?? throw new InvalidOperationException("Book not found.");

        var member = _memberRepository.GetById(request.MemberId)
            ?? throw new InvalidOperationException("Member not found.");

        if (book.AvailableCopies <= 0)
            throw new InvalidOperationException("No copies of this book are available.");

        if (_borrowRecordRepository.HasActiveBorrow(request.MemberId, request.BookId))
            throw new InvalidOperationException("Member already has an active borrow for this book.");

        book.AvailableCopies--;
        _bookRepository.Update(book);

        var record = new BorrowRecord
        {
            Id = Guid.NewGuid(),
            BookId = request.BookId,
            MemberId = request.MemberId,
            BorrowDate = DateTime.UtcNow,
            Status = "Borrowed",
            Book = book,
            Member = member
        };

        var created = _borrowRecordRepository.Add(record);
        return MapToResponse(created);
    }

    public BorrowRecordResponse ReturnBook(Guid borrowRecordId)
    {
        var record = _borrowRecordRepository.GetById(borrowRecordId)
            ?? throw new InvalidOperationException("Borrow record not found.");

        if (record.Status != "Borrowed")
            throw new InvalidOperationException("This book has already been returned.");

        record.Status = "Returned";
        record.ReturnDate = DateTime.UtcNow;

        record.Book!.AvailableCopies++;
        _bookRepository.Update(record.Book);

        var updated = _borrowRecordRepository.Update(record);
        return MapToResponse(updated);
    }

    private static BorrowRecordResponse MapToResponse(BorrowRecord br) => new()
    {
        Id = br.Id,
        BookId = br.BookId,
        MemberId = br.MemberId,
        BookTitle = br.Book?.Title ?? string.Empty,
        MemberFullName = br.Member?.FullName ?? string.Empty,
        BorrowDate = br.BorrowDate,
        ReturnDate = br.ReturnDate,
        Status = br.Status
    };
}