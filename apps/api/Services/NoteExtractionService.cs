using Ibernia.Assessment.Api.Models;

namespace Ibernia.Assessment.Api.Services;

public sealed class NoteExtractionService : INoteExtractionService
{
    public Task<ExtractedNote> ExtractAsync(string notes, CancellationToken cancellationToken)
    {
        // TODO: Candidate implementation.
        throw new NotImplementedException("Implement AI-backed note extraction.");
    }
}
