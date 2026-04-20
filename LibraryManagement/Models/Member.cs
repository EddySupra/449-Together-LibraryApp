namespace LibraryManagement.Api.Models
{
    public class Member
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Active";
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
