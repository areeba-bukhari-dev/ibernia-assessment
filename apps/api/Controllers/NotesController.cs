using Ibernia.Assessment.Api.Models;
using Ibernia.Assessment.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibernia.Assessment.Api.Controllers;

[ApiController]
[Route("api/notes")]
public sealed class NotesController(INoteExtractionService extractionService) : ControllerBase
{
    [HttpPost("extract")]
    public async Task<ActionResult<ExtractedNote>> Extract(
        [FromBody] ExtractNoteRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Notes))
        {
            return BadRequest(new { error = "Notes are required." });
        }

        var result = await extractionService.ExtractAsync(request.Notes, cancellationToken);
        return Ok(result);
    }
}
