import React, { useState } from 'react';
import { createRoot } from 'react-dom/client';
import './styles.css';

type ExtractedNote = {
  goals: string[];
  financialFacts: Record<string, number>;
  futureEvents: string[];
  risksOrQuestions: string[];
};

function App() {
  const [notes, setNotes] = useState('');
  const [result, setResult] = useState<ExtractedNote | null>(null);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  async function handleExtract() {
    setError('');
    setResult(null);
    setIsLoading(true);

    try {
      const response = await fetch(
        `${import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'}/api/notes/extract`,
        {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ notes }),
        },
      );

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.error ?? 'The extraction request failed.');
      }

      setResult(data as ExtractedNote);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Something went wrong.');
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <main className="page">
      <section className="card">
        <p className="eyebrow">IBERNIA • ENGINEERING ASSESSMENT</p>
        <h1>Advisor Note Extractor</h1>
        <p className="muted">
          Build the experience that turns adviser meeting notes into safe,
          structured financial information.
        </p>

        <label htmlFor="notes">Meeting notes</label>
        <textarea
          id="notes"
          value={notes}
          onChange={(event) => setNotes(event.target.value)}
          placeholder="Paste adviser/client meeting notes here..."
          rows={12}
        />

        <button
          type="button"
          onClick={handleExtract}
          disabled={!notes.trim() || isLoading}
        >
          {isLoading ? 'Extracting…' : 'Extract information'}
        </button>

        {error && (
          <p className="error" role="alert">
            {error}
          </p>
        )}

        {result && (
          <section className="results" aria-live="polite">
            <h2>Extracted information</h2>

            <h3>Goals</h3>
            {result.goals.length > 0 ? (
              <ul>
                {result.goals.map((goal, index) => (
                  <li key={`goal-${index}`}>{goal}</li>
                ))}
              </ul>
            ) : (
              <p>None identified.</p>
            )}

            <h3>Financial facts</h3>
            {Object.keys(result.financialFacts).length > 0 ? (
              <ul>
                {Object.entries(result.financialFacts).map(([name, value]) => (
                  <li key={name}>
                    {name}: {value}
                  </li>
                ))}
              </ul>
            ) : (
              <p>None identified.</p>
            )}

            <h3>Future events</h3>
            {result.futureEvents.length > 0 ? (
              <ul>
                {result.futureEvents.map((event, index) => (
                  <li key={`event-${index}`}>{event}</li>
                ))}
              </ul>
            ) : (
              <p>None identified.</p>
            )}

            <h3>Risks or questions</h3>
            {result.risksOrQuestions.length > 0 ? (
              <ul>
                {result.risksOrQuestions.map((item, index) => (
                  <li key={`risk-${index}`}>{item}</li>
                ))}
              </ul>
            ) : (
              <p>None identified.</p>
            )}
          </section>
        )}
{result && !error && (
        <p className="hint">
          Review the extracted information for accuracy before relying on it.
        </p>
        )}
      </section>
    </main>
  );
}

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
);