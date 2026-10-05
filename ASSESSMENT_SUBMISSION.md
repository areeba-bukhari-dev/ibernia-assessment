## Pull request
Private repository: https://github.com/areeba-bukhari-dev/ibernia-assessment
Pull request: https://github.com/areeba-bukhari-dev/ibernia-assessment/pull/1

## Deployed URL
Frontend: https://ibernia-assessment-web.netlify.app
API: https://ibernia-notes-api.runasp.net
Health check: https://ibernia-notes-api.runasp.net/health

## What I built
A web app for submitting adviser meeting notes and reviewing extracted goals,
financial facts, future events, and risks or questions. A .NET API calls the AI
provider and returns structured results.

## Key technical decisions
- Keep the AI credential on the API host; never place it in frontend code or Git.
- Return evidence from the notes with extracted information.
- Keep missing information missing and preserve uncertainty.


## Testing performed
- API health check returned {"status":"ok"}.
- Tested extraction on the deployed web app using fictional notes; structured
  information was returned.
- Automated tests: includes check for oversized notes are rejected,  check for blank notes are rejected , check for valid notes produce extracted information.

## Deployment / environment
The React/Vite frontend is deployed on Netlify. The .NET API is deployed on
MonsterASP. The API credential is configured server-side.

## Known limitations
- no authentication or rate limiting.
- Extraction results need adviser review.

## What I would do next
- Add authentication and rate limiting before broader public use.
- Expand testing and monitoring.