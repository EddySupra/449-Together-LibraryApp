namespace LibraryManagement.Api.Dtos;

public class CreateBorrowRequest
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
}

public class BorrowRecordResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public string MemberFullName { get; set; } = string.Empty;
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty;
}