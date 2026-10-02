namespace Ibernia.Assessment.Api.Models;

public sealed record ExtractedNote(
    IReadOnlyList<string> Goals,
    IReadOnlyDictionary<string, decimal> FinancialFacts,
    IReadOnlyList<string> FutureEvents,
    IReadOnlyList<string> RisksOrQuestions);
