namespace LibraryManagement.Api.Dtos;

public class CreateBorrowRequest
{
    public Guid MemberId { get; set; }
    public Guid BookId { get; set; }
}

public class BorrowRecordResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public string MemberFullName { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
    public DateTime DueAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}
