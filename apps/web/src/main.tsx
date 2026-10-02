import React, { useState } from 'react';
import { createRoot } from 'react-dom/client';
import './styles.css';

function App() {
  const [notes, setNotes] = useState('');

  return (
    <main className="page">
      <section className="card">
        <p className="eyebrow">IBERNIA · ENGINEERING ASSESSMENT</p>
        <h1>Advisor Note Extractor</h1>
        <p className="muted">
          Build the experience that turns adviser meeting notes into safe, structured financial information.
        </p>

        <label htmlFor="notes">Meeting notes</label>
        <textarea
          id="notes"
          value={notes}
          onChange={(event) => setNotes(event.target.value)}
          placeholder="Paste adviser/client meeting notes here..."
          rows={12}
        />

        <button type="button" disabled>
          Extract information
        </button>

        <p className="hint">The interface is intentionally incomplete. Implement the end-to-end feature.</p>
      </section>
    </main>
  );
}

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
);
