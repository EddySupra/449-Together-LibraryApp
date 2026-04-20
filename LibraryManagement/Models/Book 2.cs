namespace LibraryManagement.Api.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public int CopiesAvailable { get; set; }
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
