using Ibernia.Assessment.Api.Models;
using Ibernia.Assessment.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibernia.Assessment.Api.Controllers;

[ApiController]
[Route("api/notes")]
public sealed class NotesController(INoteExtractionService extractionService) : ControllerBase
{
    private const int MaxNotesLength = 12_000;

    private static readonly string MaxNotesLengthErrorMessage = $"Notes must be less than {MaxNotesLength:N0} characters.";

    [HttpPost("extract")]
    public async Task<ActionResult<ExtractedNote>> Extract(
        [FromBody] ExtractNoteRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Notes))
            {
                return BadRequest(new { error = "Notes are required." });
            }
            if (request.Notes.Length > MaxNotesLength)
            {
                return BadRequest(new { error = MaxNotesLengthErrorMessage });
            }
            var result = await extractionService.ExtractAsync(request.Notes, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Catch the error and return a clean message
            // You can log 'ex.Message' here if you have a logger
            return StatusCode(500, new { error = "Failed to extract notes. The Service is not working right now, please try again later." });
        }
    }
}
