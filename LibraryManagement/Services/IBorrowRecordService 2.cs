using LibraryManagement.Api.Dtos;

namespace LibraryManagement.Api.Services;

public interface IBorrowService
{
    IEnumerable<BorrowRecordResponse> GetBorrowRecords();
    IEnumerable<BorrowRecordResponse> GetBorrowRecordsByMemberId(Guid memberId);
    BorrowRecordResponse BorrowBook(CreateBorrowRequest request);
    BorrowRecordResponse ReturnBook(Guid borrowRecordId);
}
