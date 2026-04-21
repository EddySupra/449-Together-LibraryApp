using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Api.Dtos;
using LibraryManagement.Api.Services;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/borrowing")]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowRecordService _borrowService;

    public BorrowingController(IBorrowRecordService borrowService)
    {
        _borrowService = borrowService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<BorrowRecordResponse>> GetBorrowRecords()
    {
        var records = _borrowService.GetBorrowRecords();
        return Ok(records);
    }

    [HttpGet("history/{memberId:guid}")]
    public ActionResult<IEnumerable<BorrowRecordResponse>> GetMemberHistory(Guid memberId)
    {
        var history = _borrowService.GetBorrowRecordsByMemberId(memberId);
        return Ok(history);
    }

    [HttpPost("borrow")]
    public ActionResult<BorrowRecordResponse> BorrowBook([FromBody] CreateBorrowRequest request)
    {

        if (request == null)
        {
            return BadRequest(new { error = "Request body cannot be empty." });
        }
        var result = _borrowService.BorrowBook(request);
        return Ok(result);

    }

    [HttpPost("return/{id:guid}")]
    public ActionResult<BorrowRecordResponse> ReturnBook(Guid id)
    {

        var result = _borrowService.ReturnBook(id);

        if (result == null)
        {
            return NotFound(new { error = $"Borrow record with ID {id} was not found." });
        }

        return Ok(result);
    }

}
