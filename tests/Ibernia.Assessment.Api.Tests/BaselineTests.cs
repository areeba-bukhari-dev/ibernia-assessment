using Ibernia.Assessment.Api.Controllers;
using Ibernia.Assessment.Api.Models;
using Ibernia.Assessment.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibernia.Assessment.Api.Tests;

public sealed class BaselineTests
{
    [Fact]
    public void TestInfrastructureIsWorking()
    {
        Assert.True(true);
    }
    [Fact]
public async Task Extract_ReturnsBadRequest_WhenNotesExceedLimit()
{
    var controller = new NotesController(new NeverCalledExtractionService());
    var notes = new string('a', 12_001);

    var response = await controller.Extract(
        new ExtractNoteRequest(notes),
        CancellationToken.None);

    Assert.IsType<BadRequestObjectResult>(response.Result);
}


[Fact]
public async Task Extract_ReturnsBadRequest_WhenNotesAreBlank()
{
    var controller = new NotesController(new NeverCalledExtractionService());

    var response = await controller.Extract(
        new ExtractNoteRequest("   "),
        CancellationToken.None);

    Assert.IsType<BadRequestObjectResult>(response.Result);
}

[Fact]
public async Task Extract_ReturnsExtractedNote_WhenNotesAreValid()
{
    var expected = new ExtractedNote(
        new[] { "Retire at 60" },
        new Dictionary<string, decimal> { ["savings"] = 250000m },
        Array.Empty<string>(),
        Array.Empty<string>());

    var fakeService = new StubExtractionService(expected);
    var controller = new NotesController(fakeService);

    var response = await controller.Extract(
        new ExtractNoteRequest("Client wants to retire at 60."),
        CancellationToken.None);

    var ok = Assert.IsType<OkObjectResult>(response.Result);
    Assert.Same(expected, ok.Value);
    Assert.Equal("Client wants to retire at 60.", fakeService.ReceivedNotes);
}

private sealed class NeverCalledExtractionService : INoteExtractionService
{
    public Task<ExtractedNote> ExtractAsync(
        string notes,
        CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("The service should not be called for overlong notes.");
    }
}

private sealed class StubExtractionService(ExtractedNote result)
    : INoteExtractionService
{
    public string? ReceivedNotes { get; private set; }

    public Task<ExtractedNote> ExtractAsync(
        string notes,
        CancellationToken cancellationToken)
    {
        ReceivedNotes = notes;
        return Task.FromResult(result);
    }
}
}
