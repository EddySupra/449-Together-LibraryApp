using LibraryManagement.Api.Data;
using LibraryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Api.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly ApplicationDbContext _context;

    public BorrowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<BorrowRecord> GetAll()
    {
        return _context.BorrowRecords
            .Include(br => br.Member)
            .Include(br => br.Book)
            .ToList();
    }

    public BorrowRecord? GetById(Guid id)
    {
        return _context.BorrowRecords
            .Include(br => br.Member)
            .Include(br => br.Book)
            .FirstOrDefault(br => br.Id == id);
    }

    public IEnumerable<BorrowRecord> GetByMemberId(Guid memberId)
    {
        return _context.BorrowRecords
            .Include(br => br.Book)
            .Where(br => br.MemberId == memberId)
            .ToList();
    }

    public BorrowRecord Add(BorrowRecord record)
    {
        _context.BorrowRecords.Add(record);
        _context.SaveChanges();
        return record;
    }

    public BorrowRecord Update(BorrowRecord record)
    {
        _context.BorrowRecords.Update(record);
        _context.SaveChanges();
        return record;
    }

    public bool HasActiveBorrow(Guid memberId, Guid bookId)
    {
        return _context.BorrowRecords.Any(br =>
            br.MemberId == memberId &&
            br.BookId == bookId &&
            br.Status == "Borrowed");
    }
}
