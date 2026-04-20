using LibraryManagement.Api.Models;

namespace LibraryManagement.Api.Repositories;

public interface IBorrowRepository
{
    IEnumerable<BorrowRecord> GetAll();
    BorrowRecord? GetById(Guid id);
    IEnumerable<BorrowRecord> GetByMemberId(Guid memberId);
    BorrowRecord Add(BorrowRecord record);
    BorrowRecord Update(BorrowRecord record);
    bool HasActiveBorrow(Guid memberId, Guid bookId);
}
