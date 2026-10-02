using Ibernia.Assessment.Api.Models;

namespace Ibernia.Assessment.Api.Services;

public interface INoteExtractionService
{
    Task<ExtractedNote> ExtractAsync(string notes, CancellationToken cancellationToken);
}
