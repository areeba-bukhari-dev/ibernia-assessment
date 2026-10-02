# Ibernia Engineering Assessment

This exercise is designed to evaluate how you deliver a production-oriented feature end to end.

You are expected to use AI-assisted engineering tools. We care about how you reason, validate, test, and ship—not whether you write every line manually.

## Scenario

Ibernia is building tools for financial advisers. Advisers often write free-form notes after speaking with a client. We want to turn those notes into structured information that can be reviewed by an adviser.

Your task is to implement an **Advisor Note Extractor**.

An adviser should be able to paste meeting notes and receive structured output containing information such as:

- goals
- financial facts
- future events
- risks or questions

Example input:

```text
John wants to retire at 62. His current pension is £420,000.
He is worried about whether £55,000 per year is sustainable in retirement.
He may sell his second property in five years.
```

A reasonable output could look like:

```json
{
  "goals": ["Retire at age 62"],
  "financialFacts": {
    "pension": 420000,
    "desiredAnnualSpending": 55000
  },
  "futureEvents": ["Potential property sale in five years"],
  "risksOrQuestions": ["Retirement spending sustainability"]
}
```

The exact schema is yours to design. Explain your decisions.

## Your responsibility

Treat this as a small production feature, not a coding puzzle.

You own the full delivery lifecycle:

1. Understand the requirement.
2. Identify ambiguities and document assumptions.
3. Propose a lightweight technical design.
4. Implement the feature.
5. Add appropriate tests.
6. Work in your own private repository and open a pull request there with a useful description.
7. Configure CI so the relevant checks run automatically.
8. Deploy the application.
9. Verify the deployed feature works.
10. Document anything you would improve before a real production release.

## AI/tool usage

**AI-assisted development is expected and encouraged.**

You may use Cursor, Claude Code, Codex, ChatGPT, GitHub Copilot, documentation, search, MCP tools, or other tools you would normally use.

Create an `AI_WORKLOG.md` containing a concise record of:

- tools used
- important prompts or instructions
- notable AI-generated suggestions you accepted
- notable AI-generated suggestions you rejected or changed
- bugs or problems discovered through review/testing

Do not paste every interaction. We want to understand how you directed and validated the tools.

## Functional expectations

At minimum:

- A user can submit adviser/client meeting notes.
- The backend processes the notes using an LLM or AI provider.
- The response is structured and validated before being returned to the caller.
- Missing information must remain missing; the application must not invent financial facts.
- AI/provider failures must be handled safely.
- Invalid model responses must not crash the application.
- The user receives understandable feedback when processing fails.

## Security expectations

Assume the submitted notes may contain sensitive personal and financial information.

You should consider:

- secrets management
- logging
- accidental exposure of note contents
- prompt injection / untrusted note content
- input validation
- authorization boundaries, even if authentication itself is out of scope

## Testing expectations

Choose the test coverage you believe is appropriate. We expect more than a happy-path test.

Consider scenarios such as:

- valid structured extraction
- incomplete notes
- approximate or ambiguous values
- malformed AI output
- AI timeout/provider error
- malicious or instruction-like text inside the notes

## Deployment

Deploy the solution to a publicly reachable test environment. Railway is preferred, but another reasonable platform is acceptable.

Your submission must include:

- a usable web interface for the adviser workflow, and
- a reachable backend extraction endpoint.

The frontend and backend may use separate deployment URLs if you prefer; document both clearly in `ASSESSMENT_SUBMISSION.md`.

Secrets must not be committed to GitHub.

You may use any LLM/AI provider you can reasonably integrate. You are responsible for configuring provider credentials in your local/deployed environment. If you do not have access to an AI provider, tell us before starting so we can provide a temporary assessment credential separately. Do not put credentials in source code, documentation, commits, screenshots, or logs.

## Submission repository

Do **not** open your solution as a pull request against this public starter repository.

Create your own **private** repository from this starter, complete the assessment there, and give the Ibernia evaluator access to that repository. Open your assessment pull request inside your private repository so we can review your branch, commits, PR description, and CI history without exposing your solution to future candidates.

## Deliverables

Submit:

- private GitHub repository and pull request
- deployed URL
- `docs/DESIGN.md`
- `AI_WORKLOG.md`
- automated tests
- short PR summary covering:
  - what you built
  - key decisions
  - how you tested it
  - deployment details
  - known limitations

## Timebox

Target **3–4 hours** of focused work.

If you cannot finish everything, submit what you have. Explain what remains and what you would do next. A well-reasoned partial solution is more useful than hidden unfinished work.

## Starting the repository

The starter contains:

- `.NET 9` API
- a small React/Vite frontend shell
- test project
- GitHub Actions starter workflow
- Docker support

You may change the structure if you can justify the change.

### Backend

```bash
cd apps/api
dotnet restore
dotnet run
```

### Frontend

```bash
cd apps/web
npm install
npm run dev
```

### Tests

From the repository root:

```bash
dotnet test tests/Ibernia.Assessment.Api.Tests/Ibernia.Assessment.Api.Tests.csproj
```

Good luck. We are interested in how you work as much as the final code.
