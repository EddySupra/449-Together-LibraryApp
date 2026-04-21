using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Services;

namespace LibraryManagement.Api.Controllers;
[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BookResponse>>GetBooks()
    {
        var books = _bookService.GetBooks();
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<BookResponse>GetBookById(Guid id)
    {
        var response = _bookService.GetBookById(id);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    public ActionResult<BookResponse> CreateEvent([FromBody] CreateBookRequest input)
    {
        var created = _bookService.CreateBook(input);
        return CreatedAtAction(nameof(GetBookById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<BookResponse> UpdateBook(Guid id, [FromBody] UpdateBookRequest input)
    {
        var updated = _bookService.UpdateBook(id, input);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public ActionResult DeleteBook(Guid id)
    {
        _bookService.DeleteBook(id);
        
        return NoContent();
    }
}
