namespace LibraryManagement.Api.Models
{
    public class BorrowRecord
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; } = "Borrowed";
        public Member? Member { get; set; }
        public Book? Book { get; set; }
    }
}
