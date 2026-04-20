namespace LibraryManagement.Api.Dtos;

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public int CopiesAvailable { get; set; }
}

public class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public int CopiesAvailable { get; set; }
}
